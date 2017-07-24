using System;
using System.Collections.Generic;
using System.IO;
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
            var documentRoot = "";

            Console.WriteLine("Do you want to edit default Document Root? y/n");
            var answerForDocumentRoot = Console.ReadLine();
            if (answerForDocumentRoot == "y")
            {
                Console.WriteLine("Enter new Document Root");
                documentRoot = Console.ReadLine();
            }
            else
            {
                documentRoot = "Z:/Robertas/VisualStudio/Codec8Decoder/das/Content/";
            }
            Console.WriteLine("Document Root: " + documentRoot);

            var filePath = documentRoot;

            Console.WriteLine("Enter port number: ");    
            var portNumber = Convert.ToInt32(Console.ReadLine());
            if (portNumber < 0 || portNumber > 65535)
            {
                Console.WriteLine("Port number is wrong. Set to default '80'");
                portNumber = 80;
            }

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ipAddress, portNumber);
            Console.WriteLine(string.Format("Listening port:{0}", portNumber));
            Console.WriteLine();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endpoint);
            
            socket.Listen(10);
            
            Console.WriteLine("Waiting for http requests...\r\n");

            while(true)
            {

                Socket clientsocket = socket.Accept();

                Console.WriteLine("Client connected: {0}\r\n", clientsocket.RemoteEndPoint);
                byte[] buffer = new byte[1024];
                int receiveLength = clientsocket.Receive(buffer, 1024, SocketFlags.None);
                string requestString = Encoding.UTF8.GetString(buffer, 0, receiveLength);

                string[] st = requestString.Split();

                string file = st[1];
                

                if (file.EndsWith("/"))
                {
                    filePath += "index.html";
                }
                else
                {
                    filePath += file;
                }

                Console.WriteLine(requestString);

                if (File.Exists(filePath))
                {
                    string statusLine = "HTTP/1.1 200 OK\r\n";
                    string  responseHeader = "Content-Type: text/html\r\n";
                    clientsocket.Send(Encoding.UTF8.GetBytes(statusLine));
                    clientsocket.Send(Encoding.UTF8.GetBytes(responseHeader));
                    clientsocket.Send(Encoding.UTF8.GetBytes("\r\n"));
                    clientsocket.SendFile(filePath);
                    clientsocket.Close();
                }
                else
                {
                    string statusLine = "HTTP/1.1 404 Not Found\r\n";
                    string responseHeader = "Content-Type: text/html\r\n";
                    clientsocket.Send(Encoding.UTF8.GetBytes(statusLine));
                    clientsocket.Send(Encoding.UTF8.GetBytes(responseHeader));
                    clientsocket.Send(Encoding.UTF8.GetBytes("\r\n"));
                    clientsocket.Send(Encoding.UTF8.GetBytes("<html><head><title>Not Found</title></head><body><div>Not Found</div></body></html>"));
                    clientsocket.Close();
                }
               
                
            }
            
            Console.ReadLine();
            socket.Close();
        }
    }
}

