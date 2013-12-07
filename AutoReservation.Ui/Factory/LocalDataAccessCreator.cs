using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
    class LocalDataAccessCreator: Creator
    {
        public override Common.Interfaces.IAutoReservationService CreateInstance()
        {
			AutoReservationService local = new AutoReservationService();
			return local;
        }
    }
}
