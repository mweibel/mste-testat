﻿using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
	[DataContract]
	[KnownType(typeof (AutoDto))]
	[KnownType(typeof (KundeDto))]
	public class ReservationDto : DtoBase
	{
		private AutoDto _auto;
		private DateTime _bis;
		private KundeDto _kunde;
		private int _reservationNr;
		private DateTime _von;

		[DataMember]
		public DateTime Bis
		{
			get { return _bis; }
			set
			{
			    if (_bis == value)
			    {
			        return;
			    }
			    SendPropertyChanging(() => Bis);
			    _bis = value;
			    SendPropertyChanged(() => Bis);
			}
		}

		[DataMember]
		public DateTime Von
		{
			get { return _von; }
			set
			{
			    if (_von == value)
			    {
			        return;
			    }
			    SendPropertyChanging(() => Von);
			    _von = value;
			    SendPropertyChanged(() => Von);
			}
		}

		[DataMember]
		public int ReservationNr
		{
			get { return _reservationNr; }
			set
			{
			    if (_reservationNr == value)
			    {
			        return;
			    }
			    SendPropertyChanging(() => ReservationNr);
			    _reservationNr = value;
			    SendPropertyChanged(() => ReservationNr);
			}
		}

		[DataMember]
		public AutoDto Auto
		{
			get { return _auto; }
			set
			{
			    if (_auto != null && _auto.Equals(value))
			    {
			        return;
			    }
			    SendPropertyChanging(() => Auto);
			    _auto = value;
			    SendPropertyChanged(() => Auto);
			}
		}

		[DataMember]
		public KundeDto Kunde
		{
			get { return _kunde; }
			set
			{
			    if (_kunde != null && _kunde.Equals(value))
			    {
			        return;
			    }
			    SendPropertyChanging(() => Kunde);
			    _kunde = value;
			    SendPropertyChanged(() => Kunde);
			}
		}

		public override string Validate()
		{
			var error = new StringBuilder();
			if (Von == DateTime.MinValue)
			{
				error.AppendLine("- Von-Datum ist nicht gesetzt.");
			}
			if (Bis == DateTime.MinValue)
			{
				error.AppendLine("- Bis-Datum ist nicht gesetzt.");
			}
			if (Von > Bis)
			{
				error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
			}
			if (Auto == null)
			{
				error.AppendLine("- Auto ist nicht zugewiesen.");
			}
			else
			{
				string autoError = Auto.Validate();
				if (!string.IsNullOrEmpty(autoError))
				{
					error.AppendLine(autoError);
				}
			}
			if (Kunde == null)
			{
				error.AppendLine("- Kunde ist nicht zugewiesen.");
			}
			else
			{
				string kundeError = Kunde.Validate();
				if (!string.IsNullOrEmpty(kundeError))
				{
					error.AppendLine(kundeError);
				}
			}


			return error.Length == 0 ? null : error.ToString();
		}

		public override object Clone()
		{
			return new ReservationDto
			{
				ReservationNr = ReservationNr,
				Von = Von,
				Bis = Bis,
				Auto = (AutoDto) Auto.Clone(),
				Kunde = (KundeDto) Kunde.Clone()
			};
		}

		public override string ToString()
		{
			return string.Format(
				"{0}; {1}; {2}; {3}; {4}",
				ReservationNr,
				Von,
				Bis,
				Auto,
				Kunde);
		}

		protected override int GetIdForComparison()
		{
			return ReservationNr;
		}
	}
}