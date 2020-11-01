using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HeatmapProcessingApp;

namespace HeatmapProcessingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is starting...");

            var port = 9050;
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine($"Waiting for a clients on port {port}...");

            var heatmapProcessor = new HeatmapProcessor();
            //heatmapProcessor.DoProcessing();
            while (true)
            {
                heatmapProcessor.AcceptAndServeClient(listener);
            }
        }
    }
}
