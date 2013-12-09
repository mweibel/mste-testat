using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly List<ReservationDto> reservationOriginal = new List<ReservationDto>();
        private ObservableCollection<ReservationDto> reservationen;
        public ObservableCollection<ReservationDto> Reservationen
        {
            get
            {
				if (reservationen == null)
                {
					reservationen = new ObservableCollection<ReservationDto>();
                }
				return reservationen;
            }
        }

		private readonly List<KundeDto> kundenOriginal = new List<KundeDto>();
		private ObservableCollection<KundeDto> kunden;
		public ObservableCollection<KundeDto> Kunden
		{
			get
			{
				if (kunden == null)
				{
					kunden = new ObservableCollection<KundeDto>();
				}
				return kunden;
			}
		}

		private readonly List<AutoDto> autosOriginal = new List<AutoDto>();
		private ObservableCollection<AutoDto> autos;
		public ObservableCollection<AutoDto> Autos
		{
			get
			{
				if (autos == null)
				{
					autos = new ObservableCollection<AutoDto>();
				}
				return autos;
			}
		}



        private ReservationDto selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    SendPropertyChanging(() => SelectedReservation);
                    selectedReservation = value;
                    SendPropertyChanged(() => SelectedReservation);
                }
            }
        }


        #region Load-Command

        protected override void Load()
        {
            Reservationen.Clear();
            reservationOriginal.Clear();
            foreach (ReservationDto reservation in Service.Reservationen)
            {
                Reservationen.Add(reservation);
                reservationOriginal.Add((ReservationDto)reservation.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();


			// We need the customers too, for the combobox :)
			Kunden.Clear();
			kundenOriginal.Clear();
			foreach (KundeDto kunde in Service.Kunden)
			{
				Kunden.Add(kunde);
				kundenOriginal.Add((KundeDto)kunde.Clone());
			}


			// We need the customers too, for the combobox :)
			Autos.Clear();
			autosOriginal.Clear();
			foreach (AutoDto auto in Service.Autos)
			{
				Autos.Add(auto);
				autosOriginal.Add((AutoDto)auto.Clone());
			}
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
					ReservationDto original = reservationOriginal.FirstOrDefault(ao => ao.ReservationNr == modified.ReservationNr);
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

            StringBuilder errorText = new StringBuilder();
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
            // TODO Kunden.Add(new ReservationDto { Geburtsdatum = DateTime.Today });
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
