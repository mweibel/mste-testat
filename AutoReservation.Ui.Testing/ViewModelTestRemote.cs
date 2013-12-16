using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf.Host;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Ui.Testing
{
	[TestClass]
	public class ViewModelTestRemote : ViewModelTestBase
	{
		private IAutoReservationService target;

		protected override IAutoReservationService Target
		{
			get
			{
				if (target == null)
				{
					var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
					target = channelFactory.CreateChannel();
				}
				return target;
			}
		}

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			AutoReservationServiceHost.StartService();
		}

		[ClassCleanup]
		public static void TearDown()
		{
			AutoReservationServiceHost.StopService();
		}
	}
}