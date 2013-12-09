using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    [KnownType(typeof(AutoKlasse))]
    public class AutoDto : DtoBase
    {
        private int _id;
        private string _marke;
        private int _tagestarif;
        private int _basistarif;
        private AutoKlasse _autoKlasse;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    SendPropertyChanging(() => Id);
                    _id = value;
                    SendPropertyChanged(() => Id);
                }
            }
        }

        [DataMember]
        public string Marke
        {
            get { return _marke; }
            set
            {
                if (_marke == value)
                {
                    return;
                }
                SendPropertyChanging(() => Marke);
                _marke = value;
                SendPropertyChanged(() => Marke);
            }
        }

        [DataMember]
        public int Tagestarif
        {
            get { return _tagestarif; }
            set
            {
                if (_tagestarif == value)
                {
                    return;
                }
                SendPropertyChanging(() => Tagestarif);
                _tagestarif = value;
                SendPropertyChanged(() => Tagestarif);
            }
        }

        [DataMember]
        public int Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (_basistarif == value)
                {
                    return;
                }
                SendPropertyChanging(() => Basistarif);
                _basistarif = value;
                SendPropertyChanged(() => Basistarif);
            }
        }

        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                if (_autoKlasse == value)
                {
                    return;
                }
                SendPropertyChanging(() => AutoKlasse);
                _autoKlasse = value;
                SendPropertyChanged(() => AutoKlasse);
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (Tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                AutoKlasse = AutoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                Id,
                Marke,
                Tagestarif,
                Basistarif,
                AutoKlasse);
        }


		public override bool Equals(object obj)
		{
			bool equals = false;

			if(obj.GetType() == typeof(AutoDto))
			{
				AutoDto other = (AutoDto)obj;
				equals = other.Id.Equals(this.Id);
			}

			return equals;
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
    }
}
