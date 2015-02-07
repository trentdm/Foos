using System;
using Foos.AppStart;

namespace Foos
{
    class Program
    {
        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://*:8080/" : args[0];
            var appHost = new AppHost()
                .Init()
                .Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadKey();
        }
    }
}
