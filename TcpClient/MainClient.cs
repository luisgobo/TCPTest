using SimpleTCP;
using System;
using System.Windows.Forms;
using TcpTestLN.Enums;
using TcpTestLN.Generic;
using TcpTestLN.Handlers;
using TcpTestLN.Specific;

namespace MonitorApp
{
    public partial class MainClient : Form
    {
        SimpleTcpClient client;
        string clientName = string.Empty;

        public MainClient()
        {
            InitializeComponent();
            EnableUIComponents(false);
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

        private void Client_DataReceived(object sender, SimpleTCP.Message msg)
        {
            var objData = PackageHandler<Cliente>.DeserializePackage(msg.MessageString);

            if (objData is EventPackage<Cliente>)
            {
                Cliente cliente = null;
                txtStatus.Invoke((MethodInvoker)delegate
                {
                    txtStatus.Text += "Server response Received..." + Environment.NewLine;
                    cliente = objData.GenericInstance;

                    if (cliente != null)
                        txtStatus.Text += $"Hello {cliente.NombreCompleto}!!!{Environment.NewLine}";
                    else
                        txtStatus.Text += "Sorry, client not found!!!{Environment.NewLine}";
                });
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                client.Connect(txtHost.Text, int.Parse(txtPort.Text));
                EnableUIComponents(true);
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
                //string fullMsg = clientName + " says: " + txtMessage.Text + Environment.NewLine;
                //client.WriteLineAndGetReply(fullMsg, TimeSpan.FromSeconds(3));

                txtStatus.Invoke((MethodInvoker)delegate
                {
                    txtStatus.Text += "Sending data to Server..." + Environment.NewLine;
                    var usuario = new Cliente() { IdCliente = txtMessage.Text };
                    var myEvent = new EventPackage<Cliente>()
                    {
                        ClientId = clientName,
                        ActionType = ActionTypes.GetSpecific,
                        GenericInstance = usuario
                    };

                    var request = PackageHandler<Cliente>.SerializePackage(myEvent);
                    client.WriteLineAndGetReply(request, TimeSpan.FromSeconds(3));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has happening", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableUIComponents(bool enableUI)
        {
            txtMessage.Enabled = enableUI;
            txtStatus.Enabled = enableUI;
            btnSend.Enabled = enableUI;
        }
    }
}