using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPServer
{
    public class Initializer
    {



        Thread thread = null;
        internal bool isServerRunning = true;
        bool isServerStarted = false;
        TcpListener serverSocket = null;
        public int clientCounter = 0;

        public void CloseServer()
        {
            isServerRunning = false;
            serverSocket?.Stop();
            thread?.Join();
            isServerStarted = false;
        }

        internal void StartServerThread()
        {
            isServerRunning = true;
            thread = new Thread(StartServerService);
            isServerStarted = true;
            thread.Start();
        }

        internal void CloseServerThread()
        {
            isServerRunning = false;
            serverSocket?.Stop();
            thread.Join();
            isServerStarted = false;
        }

        public void StartServerService()
        {
            serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), 14100);

            TcpClient clientSocket = default;
            serverSocket.Start();

            //ventana_inicio.mensajeLogs("Servidor Iniciado");

            int counter = 0;
            clientCounter = 0;
            //ventana_inicio.cantidadActualClientes(clientCounter);

            while (isServerRunning)
            {
                try
                {
                    counter += 1;

                    clientSocket = serverSocket.AcceptTcpClient();

                    //ventana_inicio.mensajeLogs("Cliente id." + Convert.ToString(counter) + ": se a conectado");

                    //Cliente_profesor client = new Cliente_profesor(ventana_inicio);
                    //client.iniciarCliente(clientSocket, Convert.ToString(counter));
                }
                catch (SocketException ex)
                {
                    throw ex;                    
                }
            }

            clientSocket?.Close();
            serverSocket?.Stop();

            isServerRunning = false;
            isServerStarted = false;

            //ventana_inicio.mensajeLogs("Servidor detenido");
        }
    }
}