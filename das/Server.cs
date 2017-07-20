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

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static int Main(String[] args)
        {
            StartListening();
            return 0;
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
            allDone.Set();
            Console.WriteLine("aaa");
        }

    }
}
