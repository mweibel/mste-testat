using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    class RemoteDataAccessCreator: Creator
    {
        public override Common.Interfaces.IAutoReservationService CreateInstance()
        {
            var factory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            IAutoReservationService proxy = factory.CreateChannel();

            return proxy;
        }
    }
}
