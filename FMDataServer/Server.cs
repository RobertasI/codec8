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
            
            
            Client client = new Client(server.AcceptTcpClient());
            Thread thread =  new Thread(() => client.DoSomethingWithClient());
            thread.Start();
            
            //server.Stop();
        }
    }
}
