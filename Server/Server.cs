using System;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Owin.Hosting;

namespace Server
{
    class Server
    {
        private static string categoryName = "Clients";
        private int PORT_NO = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
        private string SERVER_IP = ConfigurationManager.AppSettings["ip"];

        public static void Main(string[] args)
        {
            Server server = new Server();
            if (PerformanceCounterCategory.Exists(categoryName))
            {
                //server.StartWebApp();
                server.StartServer();
                Console.ReadLine();
            }
            else
            {
                string firstCounterName = "Clients online";
                string firstCounterHelp = "Clients online live update";
                string categoryHelp = "Clients related real time statistics";
                PerformanceCounterCategory clientsCounter = new PerformanceCounterCategory(categoryName);
                PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.SingleInstance, firstCounterName, firstCounterHelp);
                server.StartServer();
                Console.ReadLine();
            }
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

        public void StartWebApp()
        {
            string url = "http://localhost:53212";
            WebApp.Start(url);
            Console.WriteLine("Server running on {0}", url);
        }
    }
}
