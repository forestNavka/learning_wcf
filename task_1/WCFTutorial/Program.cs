using System;
using System.ServiceModel;
using System.Linq;

namespace WCFTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(CalculatorService.CalculatorService)))
            {
                host.Open();

                Console.WriteLine("The service has been started");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
