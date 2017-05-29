using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FMDataServer
{
    public class Server
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            TcpListener server = new TcpListener(IPAddress.Parse("0.0.0.0"), 66);
            server.Start();
            Console.WriteLine("Started.");
            while (true)
            {
                Client client = new Client(server.AcceptTcpClient());
                new Thread(new ThreadStart(client.DoSomethingWithClient)).Start();
            }
            server.Stop();
        }
    }
}
