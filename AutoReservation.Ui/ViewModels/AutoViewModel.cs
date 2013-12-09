using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class AutoViewModel : ViewModelBase
    {

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

        private AutoDto selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return selectedAuto; }
            set
            {
                if (selectedAuto != value)
                {
                    SendPropertyChanging(() => SelectedAuto);
                    selectedAuto = value;
                    SendPropertyChanged(() => SelectedAuto);
                }
            }
        }

        #region Load-Command

        protected override void Load()
        {
            Autos.Clear();
            autosOriginal.Clear();
            foreach (AutoDto auto in Service.Autos)
            {
                Autos.Add(auto);
                autosOriginal.Add((AutoDto)auto.Clone());
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
                    AutoDto original = autosOriginal.FirstOrDefault(ao => ao.Id == modified.Id);
                    Service.UpdateAuto(original, modified);
                }
            }
            Load();
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
            Autos.Add(new AutoDto());
        }

        protected override bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        protected override void Delete()
        {
            Service.DeleteAuto(SelectedAuto);
            Load();
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