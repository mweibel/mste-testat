﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
	public class ReservationViewModel : ViewModelBase
	{
		private readonly List<AutoDto> _autosOriginal = new List<AutoDto>();
		private readonly List<KundeDto> _kundenOriginal = new List<KundeDto>();
		private readonly List<ReservationDto> _reservationOriginal = new List<ReservationDto>();
		private ObservableCollection<AutoDto> _autos;
		private ObservableCollection<KundeDto> _kunden;
		private ObservableCollection<ReservationDto> _reservationen;
		private ReservationDto _selectedReservation;

		public ObservableCollection<ReservationDto> Reservationen
		{
			get
			{
				if (_reservationen == null)
				{
					_reservationen = new ObservableCollection<ReservationDto>();
				}
				return _reservationen;
			}
		}

		public ObservableCollection<KundeDto> Kunden
		{
			get
			{
				if (_kunden == null)
				{
					_kunden = new ObservableCollection<KundeDto>();
				}
				return _kunden;
			}
		}

		public ObservableCollection<AutoDto> Autos
		{
			get
			{
				if (_autos == null)
				{
					_autos = new ObservableCollection<AutoDto>();
				}
				return _autos;
			}
		}


		public ReservationDto SelectedReservation
		{
			get { return _selectedReservation; }
			set
			{
			    if (Equals(_selectedReservation, value))
			    {
			        return;
			    }
			    SendPropertyChanging(() => SelectedReservation);
			    _selectedReservation = value;
			    SendPropertyChanged(() => SelectedReservation);
			}
		}

		#region Load-Command

		protected override void Load()
		{
            // Load customers / cars before loading reservations, otherwise
            // incorrect references will be around.

            // We need the customers too, for the combobox :)
            Kunden.Clear();
            _kundenOriginal.Clear();
            foreach (KundeDto kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
                _kundenOriginal.Add((KundeDto)kunde.Clone());
            }

            // We need the cars too, for the combobox :)
            Autos.Clear();
            _autosOriginal.Clear();
            foreach (AutoDto auto in Service.Autos)
            {
                Autos.Add(auto);
                _autosOriginal.Add((AutoDto)auto.Clone());
            }

			Reservationen.Clear();
			_reservationOriginal.Clear();
			foreach (ReservationDto reservation in Service.Reservationen)
			{
				Reservationen.Add(reservation);
				_reservationOriginal.Add((ReservationDto) reservation.Clone());
			}
			SelectedReservation = Reservationen.FirstOrDefault();


		}

		protected override bool CanLoad()
		{
			return Service != null;
		}

		#endregion

		#region Save-Command

		protected override void SaveData()
		{
			foreach (ReservationDto modified in Reservationen)
			{
				if (modified.ReservationNr == default(int))
				{
					Service.InsertReservation(modified);
				}
				else
				{
					ReservationDto original = _reservationOriginal.FirstOrDefault(ao => ao.ReservationNr == modified.ReservationNr);
					Service.UpdateReservation(original, modified);
				}
			}
		}

		protected override bool CanSaveData()
		{
			if (Service == null)
			{
				return false;
			}

			var errorText = new StringBuilder();
			foreach (ReservationDto reservation in Reservationen)
			{
				string error = reservation.Validate();
				if (!string.IsNullOrEmpty(error))
				{
					errorText.AppendLine(reservation.ToString());
					errorText.AppendLine(error);
				}
			}

			ErrorText = errorText.ToString();
			return string.IsNullOrEmpty(ErrorText);
		}

		#endregion

		#region New-Command

		protected override void New()
		{
			var reservation = new ReservationDto
			{
				Von = DateTime.Today,
				Bis = DateTime.Today.AddMonths(1)
			};
			Reservationen.Add(reservation);
			SelectedReservation = reservation;
		}

		protected override bool CanNew()
		{
			return Service != null;
		}

		#endregion

		#region Delete-Command

		protected override void Delete()
		{
			Service.DeleteReservation(SelectedReservation);
		}

		protected override bool CanDelete()
		{
			return
				SelectedReservation != null &&
				SelectedReservation.ReservationNr != default(int) &&
				Service != null;
		}

		#endregion
	}
}