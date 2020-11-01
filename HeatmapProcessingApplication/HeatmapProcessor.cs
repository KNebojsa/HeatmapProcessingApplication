using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
                var request = reader.ReadLine();
                if (request != "Let's get connected")
                {
                    writer.Write("Conncetion refused");
                    writer.Flush();
                    return;
                }

                writer.Write("Conncetion accepted");
                writer.Flush();

                var line = string.Empty;
                var linecount = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    linecount++;
                }
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

        public void DoProcessing()
        {
            Image<Bgr, Byte> img1 = new Image<Bgr, Byte>("Heineken.jpg");// path can be absolute or relative.
            Image<Bgr, Byte> img2 = new Image<Bgr, Byte>(img1.Size.Width, img1.Size.Height);

            string[] lines = System.IO.File.ReadAllLines("Heineken.csv");
            foreach (var line in lines)
            {
                var tokens = line.Split(';');
                var column = int.Parse(tokens[1]);
                var row = int.Parse(tokens[2]);
                if (row >= 0 && row < img2.Height && column >= 0 && column < img2.Width)
                {
                    img2[row, column] = new Bgr(200, 200, 200);
                    CvInvoke.Circle(img1, new Point(column, row), 5, new MCvScalar(1.0, 1.0, 0.3), 1,LineType.AntiAlias);
                }
            }
            img1.Save("Heineken Grey.jpg");

            CvInvoke.ApplyColorMap(img2, img1, ColorMapType.Bone);
            img2.Save("AA21.png");

        }
    }
}
