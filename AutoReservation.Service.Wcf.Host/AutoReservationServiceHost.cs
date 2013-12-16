using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Host
{
	public class AutoReservationServiceHost
	{
		private static ServiceHost myServiceHost;

		public static void StartService()
		{
			//Instantiate new ServiceHost 
			myServiceHost = new ServiceHost(typeof (AutoReservationService));

			//Open myServiceHost
			myServiceHost.Open();
		}

		public static void StopService()
		{
			//Call StopService from your shutdown logic (i.e. dispose method)
			if (myServiceHost.State != CommunicationState.Closed)
				myServiceHost.Close();
		}
	}
}