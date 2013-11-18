using System;

namespace AutoReservation.Service.Wcf.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AutoReservationService starting.");
            AutoReservationServiceHost.StartService();
            Console.WriteLine("AutoReservationService started.");
            Console.WriteLine();
            Console.WriteLine("Press Return to stop the Service.");

            Console.ReadLine();
            AutoReservationServiceHost.StopService();
        }
    }
}
