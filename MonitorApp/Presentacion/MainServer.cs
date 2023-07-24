using MonitorApp.AccesoDatos;
using SimpleTCP;
using System;
using System.Net;
using System.Windows.Forms;
using TcpTestLN.Enums;
using TcpTestLN.Generic;
using TcpTestLN.Handlers;
using TcpTestLN.Specific;

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
            var objData = PackageHandler<Cliente>.DeserializePackage(msg.MessageString);

            //ResponseHandler(objData);

            switch (objData)
            {
                case EventPackage<Cliente> cli:
                    switch (cli.ActionType)
                    {
                        case ActionTypes.Add:
                            break;

                        case ActionTypes.Edit:
                            break;

                        case ActionTypes.Delete:
                            break;

                        case ActionTypes.List:
                            break;

                        case ActionTypes.GetSpecific:

                            txtStatus.Invoke((MethodInvoker)delegate
                            {
                                txtStatus.Text += $"Client request Received, procesing request...{Environment.NewLine}";
                                string statusResponse = string.Empty;

                                var clienteAD = new ClienteAD();
                                var cliente = clienteAD.GetClienteById(cli.GenericInstance.IdCliente);

                                if (cliente != null)
                                {
                                    cli.GenericInstance = cliente;
                                    //Serialize and package Object gotten
                                    string serializedResult = PackageHandler<Cliente>.SerializePackage(cli);
                                    msg.ReplyLine(serializedResult);
                                    statusResponse = $"Response sent...{Environment.NewLine}";
                                }
                                else
                                {
                                    statusResponse = "Client not found {Environment.NewLine}";
                                }

                                txtStatus.Text += statusResponse;
                            });

                            break;

                        default:
                            break;
                    }

                    break;

                default:
                    break;
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