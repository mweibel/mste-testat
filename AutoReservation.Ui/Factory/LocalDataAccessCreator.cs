using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
	internal class LocalDataAccessCreator : Creator
	{
		public override IAutoReservationService CreateInstance()
		{
			var local = new AutoReservationService();
			return local;
		}
	}
}