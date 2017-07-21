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
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void Main(string[] args)
        {

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ipAddress, 11111);
            Console.WriteLine(string.Format("Listening port:{0}", 11111));
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
            string responseBody = "<html><head><title>Hello World!</title></head><body><div>Hello World!</div></body></html>";

            clientsocket.Send(Encoding.UTF8.GetBytes(statusLine));
            clientsocket.Send(Encoding.UTF8.GetBytes(responseHeader));
            clientsocket.Send(Encoding.UTF8.GetBytes("\r\n"));
            clientsocket.Send(Encoding.UTF8.GetBytes(responseBody));

            clientsocket.Close();

            Console.ReadLine();

            socket.Close();


            //StartListening();
        }

        public static void StartListening()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(100);


            while (true)
            {
                allDone.Reset();

                Console.WriteLine("Waiting for connections...");
                listener.BeginAccept(new AsyncCallback(AcceptCallBack), listener);
                allDone.WaitOne();
            }
        }

        public static void AcceptCallBack(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            string content = string.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }
    }
}

