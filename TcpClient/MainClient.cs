using Entidades;
using Entidades.Enums;
using Entidades.Generic;
using Newtonsoft.Json;
using SimpleTCP;
using System;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MonitorApp
{
    public partial class MainClient : Form
    {
        SimpleTcpClient client;
        string clientName = string.Empty;


        public MainClient()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();

            client = new SimpleTcpClient
            {
                StringEncoder = System.Text.Encoding.UTF8,
                Delimiter = 0x13              
            };
            client.DataReceived += Client_DataReceived;
            
            clientName = "CLI-" + rnd.Next(10);
            lblHelloUser.Text = "Hello " + clientName;
        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtMessage.Invoke((MethodInvoker)delegate
            {
                txtStatus.Text += e.MessageString.TrimEnd('\u0013');
            });
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                client.Connect(txtHost.Text, int.Parse(txtPort.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error happened", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnConnect.Enabled = true;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string fullMsg = clientName + " says: " + txtMessage.Text + Environment.NewLine;
                //client.WriteLineAndGetReply(fullMsg, TimeSpan.FromSeconds(3));
                var usuario = new Cliente() { IdCliente = txtMessage.Text };

                var myEvent = new EventPackage<Cliente>()
                { 
                    ClientId = clientName,
                    ActionType = ActionTypes.GetSpecific,
                    GenericInstance = usuario
                };

                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var json = JsonConvert.SerializeObject(myEvent, jsonSerializerSettings);
                client.WriteLineAndGetReply(json, TimeSpan.FromSeconds(3));



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"An error has happening",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

    }    
}