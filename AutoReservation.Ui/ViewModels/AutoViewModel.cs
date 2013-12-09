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
    public class AutoViewModel : ViewModelBase
    {

        private readonly List<AutoDto> _autosOriginal = new List<AutoDto>();
        private ObservableCollection<AutoDto> _autos;
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

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set
            {
                if (!Equals(_selectedAuto, value))
                {
                    SendPropertyChanging(() => SelectedAuto);
                    _selectedAuto = value;
                    SendPropertyChanged(() => SelectedAuto);
                }
            }
        }

        #region Load-Command

        protected override void Load()
        {
            Autos.Clear();
            _autosOriginal.Clear();
            foreach (AutoDto auto in Service.Autos)
            {
                Autos.Add(auto);
                _autosOriginal.Add((AutoDto)auto.Clone());
            }
            SelectedAuto = Autos.FirstOrDefault();
        }

        protected override bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        protected override void SaveData()
        {
            foreach (AutoDto modified in Autos)
            {
                if (modified.Id == default(int))
                {
                    Service.InsertAuto(modified);
                }
                else
                {
                    AutoDto original = _autosOriginal.FirstOrDefault(ao => ao.Id == modified.Id);
                    Service.UpdateAuto(original, modified);
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
            foreach (AutoDto auto in Autos)
            {
                string error = auto.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(auto.ToString());
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
            AutoDto auto = new AutoDto();
            Autos.Add(auto);
            SelectedAuto = auto;
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
				Service.DeleteAuto(SelectedAuto);
	        }
	        catch (FaultException<RelationExistsException> e)
	        {
		        MessageBox.Show(
			        "Auto konnte nicht gelöscht werden." + Environment.NewLine + Environment.NewLine + e.Message,
					"Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
	        }
        }

        protected override bool CanDelete()
        {
            return
                SelectedAuto != null &&
                SelectedAuto.Id != default(int) &&
                Service != null;
        }

        #endregion

    }
}