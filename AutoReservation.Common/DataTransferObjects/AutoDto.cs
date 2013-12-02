using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    [KnownType(typeof(AutoKlasse))]
    public class AutoDto : DtoBase
    {
        private int id;
        private string marke;
        private int tagestarif;
        private int basistarif;
        private AutoKlasse autoKlasse;

        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    SendPropertyChanging(() => Id);
                    id = value;
                    SendPropertyChanged(() => Id);
                }
            }
        }

        [DataMember]
        public string Marke
        {
            get { return marke; }
            set
            {
                if (marke != value)
                {
                    SendPropertyChanging(() => Marke);
                    marke = value;
                    SendPropertyChanged(() => Marke);
                }
            }
        }

        [DataMember]
        public int Tagestarif
        {
            get { return tagestarif; }
            set
            {
                if (tagestarif != value)
                {
                    SendPropertyChanging(() => Tagestarif);
                    tagestarif = value;
                    SendPropertyChanged(() => Tagestarif);
                }
            }
        }

        [DataMember]
        public int Basistarif
        {
            get { return basistarif; }
            set
            {
                if (basistarif != value)
                {
                    SendPropertyChanging(() => Basistarif);
                    basistarif = value;
                    SendPropertyChanged(() => Basistarif);
                }
            }
        }

        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get { return autoKlasse; }
            set
            {
                if (autoKlasse != value)
                {
                    SendPropertyChanging(() => AutoKlasse);
                    autoKlasse = value;
                    SendPropertyChanged(() => AutoKlasse);
                }
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

    }
}
