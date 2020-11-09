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
        private const string ImageRecievedName = "Image received.jpg";
        private const string ImageProcessedName = "Image processed.jpg";

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
                Console.WriteLine("Conncetion accepted");

                ReceiveImage(stream);
                SendMessage(writer, "Received");

                var lines = ReceiveCsv(stream, reader);
                ProcessImage(lines);
                
                client.Client.SendFile(ImageProcessedName);
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
            using (var output = File.Create(ImageRecievedName))
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

        private void ProcessImage(List<string> lines)
        {
            Mat imageOriginal = CvInvoke.Imread(ImageRecievedName, LoadImageType.AnyColor);

            var imageWithHitsBgr = CreateHitImage(imageOriginal.Size, lines);

            // create mask to have white circles wherever hits exist and to be black on all other parts
            var mask = new Mat();
            CvInvoke.Threshold(imageWithHitsBgr, mask, 1, 255, ThresholdType.Binary);
            var inverseMask = new Mat();
            CvInvoke.BitwiseNot(mask, inverseMask);

            // mapping level of gray to ColorMap
            CvInvoke.ApplyColorMap(imageWithHitsBgr, imageWithHitsBgr, ColorMapType.Jet);
            // from mapped image remove everything except hits
            var imageWithHitsWithoutBackground = new Mat();
            CvInvoke.BitwiseAnd(imageWithHitsBgr, imageWithHitsBgr, imageWithHitsWithoutBackground, mask);

            // from original image remove only parts where hits happended 
            var imageOriginalWithoutHits = new Mat();
            CvInvoke.BitwiseAnd(imageOriginal, imageOriginal, imageOriginalWithoutHits, inverseMask);
            // result is combination of original image without hits and image with hits mapped to certain ColorMap
            var result = new Mat();
            CvInvoke.Add(imageOriginalWithoutHits, imageWithHitsWithoutBackground, result);
            result.Save(ImageProcessedName);
        }

        /// <summary>
        /// creates gray image where points with hits are brighter
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private Image<Gray, Byte> CreateHitImage(Size size, List<string> lines) 
        {
            var imageHit = new Image<Gray, Byte>(size);

            foreach (var line in lines)
            {
                var tokens = line.Split(';');
                var x = int.Parse(tokens[1]);
                var y = int.Parse(tokens[2]);
                if (y >= 0 && y < imageHit.Height && x >= 0 && x < imageHit.Width)
                {
                    var hitColor = imageHit[y, x].MCvScalar;
                    // this is configurable
                    int hitDelta = 50;
                    var newHitColor = new MCvScalar(hitDelta + hitColor.V0);
                    CvInvoke.Circle(imageHit, new Point(x, y), 25, newHitColor, -1);
                }
            }

            return imageHit;
        }
    }
}
