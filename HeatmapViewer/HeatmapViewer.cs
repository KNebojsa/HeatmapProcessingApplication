using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WfaClient
{
    public partial class HeatmapViewer : Form
    {
        private TcpClient _server;
        string imageLocation = "";

        public HeatmapViewer()
        {
            InitializeComponent();
        }
        private OpenFileDialog dialog;
        private void btnUploadViewsCsv_Click(object sender, EventArgs e)
        {
                dialog = new OpenFileDialog();
                dialog.Filter = "Csv files(*.csv)|*.csv";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    labelInserted.Text = "Coordinates added !";
                }
                else
                {
                labelInserted.Text = "Fail to add coordinates";
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private string ReadResponse(NetworkStream stream)
        {
            var bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            var readCount = stream.Read(buffer, 0, bufferSize);
            var recieved = Encoding.UTF8.GetString(buffer, 0, readCount);
            return recieved;
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        { 
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    pictureBox.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                _server = new TcpClient("127.0.0.1", 9050);
                using (var stream = _server.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.ASCII))
                {
                    writer.Write("Let's get connected");
                    writer.Flush();

                    while (!stream.DataAvailable)
                    {
                        Thread.Sleep(100);
                    }
                    var response = ReadResponse(stream);

                    if (response != "Conncetion accepted")
                    {
                        MessageBox.Show("Connection refused", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                        
                    var fileName = imageLocation;
                    _server.Client.SendFile(fileName); //sending image
                    while (!stream.DataAvailable)
                    {
                        Thread.Sleep(100);
                    }
                    var response2 = ReadResponse(stream);
                    _server.Client.SendFile(dialog.FileName); //sending coordinates

                    ReceiveImage(stream);
                    imageLocation = "ClientSideProccessed.jpg";
                    pictureBox.ImageLocation = imageLocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void ReceiveImage(NetworkStream stream)
        {
            while (!stream.DataAvailable)
            {
                Thread.Sleep(100);
            }
            using (var output = File.Create("ClientSideProccessed.jpg"))
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
    }
}
