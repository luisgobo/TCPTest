using Entidades;
using Entidades.Generic;
using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Net;
using System.Windows.Forms;

namespace MonitorApp.Presentacion
{
    public partial class MainClient : Form
    {
        SimpleTcpServer server;

        public MainClient()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer
            {
                Delimiter = 0x13,
                StringEncoder = System.Text.Encoding.UTF8
            };

            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message msg)
        {
            var cleanContent = msg.MessageString.TrimEnd('\u0013');
            txtStatus.Invoke((MethodInvoker)delegate
            {
                txtStatus.Text += cleanContent;
                msg.ReplyLine(String.Format("You said: {0}", msg.MessageString));
            });

            var jsonDeserializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var objData = JsonConvert.DeserializeObject<dynamic>(cleanContent,jsonDeserializerSettings);

            if (objData is EventPackage<Cliente>) {
                
                
                
                int i = 0; 
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server Starting..." + Environment.NewLine;            
            IPAddress ip = IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server Stoped..." + Environment.NewLine;
            server.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}