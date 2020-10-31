using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HeatmapProcessingApp
{
    public class HeatmapProcessor
    {
        public void AcceptAndServeClient(TcpListener tcpListener)
        {
            using (var client = tcpListener.AcceptTcpClient())
            {
                ServeClient(client);
            }
        }

        private void ServeClient(TcpClient client)
        {
            using (var stream = client.GetStream())
            using (var writer = new StreamWriter(stream, Encoding.ASCII))
            {
                var request = ReadConnectingRequest(stream);
                if (!request.Equals("Let's get connected"))
                {
                    writer.Write("Conncetion refused");
                    writer.Flush();
                    return;
                }

                writer.Write("Conncetion accepted");
                writer.Flush();
            }
            //using (var output = File.Create("views.csv"))
            //{
            //    // read the file in chunks of 1KB
            //    var buffer = new byte[1024];
            //    int bytesRead;
            //    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        output.Write(buffer, 0, bytesRead);
            //    }
            //}
        }

        private string ReadConnectingRequest(NetworkStream stream)
        {
            var bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            var readCount = stream.Read(buffer, 0, bufferSize);
            var recieved = Encoding.UTF8.GetString(buffer, 0, readCount);
            return recieved;
        }
    }
}
