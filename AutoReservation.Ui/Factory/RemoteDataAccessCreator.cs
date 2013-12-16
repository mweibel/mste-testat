using System.ServiceModel;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
	internal class RemoteDataAccessCreator : Creator
	{
		public override IAutoReservationService CreateInstance()
		{
			var factory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
			IAutoReservationService proxy = factory.CreateChannel();

			return proxy;
		}
	}
}