using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces.Exceptions;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        private readonly List<KundeDto> _kundenOriginal = new List<KundeDto>();
        private ObservableCollection<KundeDto> _kunden;
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

        private KundeDto _selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set
            {
                if (!Equals(_selectedKunde, value))
                {
                    SendPropertyChanging(() => SelectedKunde);
                    _selectedKunde = value;
                    SendPropertyChanged(() => SelectedKunde);
                }
            }
        }


        #region Load-Command

        protected override void Load()
        {
            Kunden.Clear();
            _kundenOriginal.Clear();
            foreach (KundeDto kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
                _kundenOriginal.Add((KundeDto)kunde.Clone());
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
                    KundeDto original = _kundenOriginal.FirstOrDefault(ao => ao.Id == modified.Id);
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
			try
			{
				Service.DeleteKunde(SelectedKunde);
			}
			catch (FaultException<RelationExistsException> e)
			{
				MessageBox.Show(
					"Kunde konnte nicht gelöscht werden." + Environment.NewLine + Environment.NewLine + e.Message,
					"Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
			}
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
