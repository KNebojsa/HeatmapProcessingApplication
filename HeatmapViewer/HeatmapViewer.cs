using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaClient
{
    public partial class HeatmapViewer : Form
    {
        private TcpClient _server;

        public HeatmapViewer()
        {
            InitializeComponent();
            //EstablishConnectionToServer();
        }

        private OpenFileDialog dialog;
        private TcpClient client;

        private void EstablishConnectionToServer()
        {
            _server = new TcpClient("127.0.0.1", 9050);
            using (var stream = _server.GetStream())
            using (var writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.Write("Let's get connected");
            }
        }


        private void btnUploadViewsCsv_Click(object sender, EventArgs e)
        {
            try
            {
                dialog = new OpenFileDialog();
                dialog.Filter = "Csv files(*.csv)|*.csv";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _server = new TcpClient("127.0.0.1", 9050);
                    using (var stream = _server.GetStream())
                    using (var writer = new StreamWriter(stream, Encoding.ASCII))
                    {
                        writer.Write("Let's get connected");
                        writer.Flush();

                        var response = ReadResponse(stream);

                        if (response != "Conncetion accepted")
                        {
                            MessageBox.Show("Connection refused", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var fileName = @"E:\Nele\Heineken.jpg";
                        _server.Client.SendFile(fileName);
                        _server.Client.SendFile(dialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
