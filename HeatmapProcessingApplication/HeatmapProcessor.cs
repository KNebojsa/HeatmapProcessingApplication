using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OpenTK.Graphics.ES20;

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
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                GetMessage(stream);
                SendMessage(writer, "Conncetion accepted");

                ReceiveImage(stream);
                SendMessage(writer, "Received");

                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>("Image.jpg");
                var lines = ReceiveCsv(stream, reader);
                foreach (var line in lines)
                {
                    var tokens = line.Split(';');
                    var column = int.Parse(tokens[1]);
                    var row = int.Parse(tokens[2]);
                    if (row >= 0 && row < img1.Height && column >= 0 && column < img1.Width)
                    {
                        CvInvoke.Circle(img1, new Point(column, row), 5, new MCvScalar(1.0, 1.0, 0.3), 1, LineType.AntiAlias);
                    }
                }

                img1.Save("Image processed.jpg");
            }
        }

        private static void SendMessage(StreamWriter writer, string message)
        {
            writer.Write(message);
            writer.Flush();
        }

        private static void ReceiveImage(NetworkStream stream)
        {
            while (!stream.DataAvailable)
            {
                Thread.Sleep(100);
            }
            using (var output = File.Create("Image.jpg"))
            {
                // read the file in chunks of 1KB
                var buffer = new byte[1024];
                int bytesRead;
                while (stream.DataAvailable)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, bytesRead);
                }
            }
        }

        private string GetMessage(NetworkStream stream)
        {
            while (!stream.DataAvailable)
            {
                Thread.Sleep(100);
            }

            var bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            var readCount = stream.Read(buffer, 0, bufferSize);
            var recieved = Encoding.UTF8.GetString(buffer, 0, readCount);
            return recieved;
        }

        private List<string> ReceiveCsv(NetworkStream stream, StreamReader reader)
        {
            var lines = new List<string>();
            while (!stream.DataAvailable)
            {
                Thread.Sleep(100);
            }

            while (stream.DataAvailable)
            {
                lines.Add(reader.ReadLine());
            }

            return lines;
        }
    }
}
