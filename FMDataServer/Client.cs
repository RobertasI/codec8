using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FMDataServer
{
    class Client
    {
        private Stream ClientStream;
        private TcpClient tcpClient;

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            ClientStream = tcpClient.GetStream();
        }

        public void DoSomethingWithClient()
        {
            StreamWriter streamwriter = new StreamWriter(ClientStream);
            StreamReader streamreader = new StreamReader(streamwriter.BaseStream);
            streamwriter.WriteLine("Hi. This is x2 TCP/IP easy-to-use server");
            streamwriter.Flush();
            string data;
            try
            {
                while ((data = streamreader.ReadLine()) != "exit")
                {
                    streamwriter.WriteLine(data);
                    streamwriter.Flush();
                }
            }
            finally
            {
                streamwriter.Close();
            }
        }
    }
}

