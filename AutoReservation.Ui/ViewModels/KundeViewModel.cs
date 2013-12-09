using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
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

        private KundeDto selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                if (selectedKunde != value)
                {
                    SendPropertyChanging(() => SelectedKunde);
                    selectedKunde = value;
                    SendPropertyChanged(() => SelectedKunde);
                }
            }
        }


        #region Load-Command

        protected override void Load()
        {
            Kunden.Clear();
            kundenOriginal.Clear();
            foreach (KundeDto kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
                kundenOriginal.Add((KundeDto)kunde.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
        }

        protected override bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        protected override void SaveData()
        {
            foreach (KundeDto modified in Kunden)
            {
                if (modified.Id == default(int))
                {
                    Service.InsertKunde(modified);
                }
                else
                {
                    KundeDto original = kundenOriginal.FirstOrDefault(ao => ao.Id == modified.Id);
                    Service.UpdateKunde(original, modified);
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
            foreach (KundeDto kunde in Kunden)
            {
                string error = kunde.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(kunde.ToString());
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
            Kunden.Add(new KundeDto { Geburtsdatum = DateTime.Today });
        }

        protected override bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        protected override void Delete()
        {
            Service.DeleteKunde(SelectedKunde);
        }

        protected override bool CanDelete()
        {
            return
                SelectedKunde != null &&
                SelectedKunde.Id != default(int) &&
                Service != null;
        }

        #endregion

    }
}
