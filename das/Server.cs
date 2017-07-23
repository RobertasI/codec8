using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace das
{
        public class Server
    {
        //https://www.codeproject.com/Articles/745134/csharp-async-socket-server

        public static void Main(string[] args)
        {

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Console.WriteLine("Enter port number: ");
            var portNumber = Console.ReadLine();
            IPEndPoint endpoint = new IPEndPoint(ipAddress, Convert.ToInt32(portNumber));
            Console.WriteLine(string.Format("Listening port:{0}", portNumber));
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endpoint);
            
            socket.Listen(10);
            

            Console.WriteLine("Waiting for http request...\r\n");
            Socket clientsocket = socket.Accept();

            Console.WriteLine("Client connected: {0}\r\n", clientsocket.RemoteEndPoint);
            byte[] buffer = new byte[1024];
            int receivelength = clientsocket.Receive(buffer, 1024, SocketFlags.None);
            string requeststring = Encoding.UTF8.GetString(buffer, 0, receivelength);

            Console.WriteLine(requeststring);

            string statusLine = "HTTP/1.1 200 OK\r\n";
            string responseHeader = "Content-Type: text/html\r\n";
            //string responseBody = "<html><head><title>Hello World!</title></head><body><div>Hello World!</div></body></html>";
            string responseBody = "~Content/index.html";


            clientsocket.Send(Encoding.UTF8.GetBytes(statusLine));
            clientsocket.Send(Encoding.UTF8.GetBytes(responseHeader));
            clientsocket.Send(Encoding.UTF8.GetBytes("\r\n"));
            clientsocket.Send(Encoding.UTF8.GetBytes(responseBody));

            clientsocket.Close();

            Console.ReadLine();

            socket.Close();


            //StartListening();
        }




        
    }
}

