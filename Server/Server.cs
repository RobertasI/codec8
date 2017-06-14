using System;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace Server
{
    class Server
    {
        private int PORT_NO = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
        private string SERVER_IP = ConfigurationManager.AppSettings["ip"]; 


        public static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer();
            ClientHandler clientHandler = new ClientHandler();
            clientHandler.checkPerformanceExistance();
            Console.ReadLine();
        }

        public void StartServer()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            listener.Start();
            Console.WriteLine("Listening... ");
            ClientHandler clientHandler = new ClientHandler();
            clientHandler.acceptClients(listener);
        }       
    }
}
