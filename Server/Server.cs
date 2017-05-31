using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        public static void Main(string[] args)
        {  
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();


            while (true)
            {

                TcpClient client = listener.AcceptTcpClient();

                using (NetworkStream nwStream = client.GetStream())
                {
                    //recieving stream data
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    var bytesCount = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                    long dataReceived = BitConverter.ToInt64(buffer, 0);
                    Console.WriteLine(bytesCount);
                    Console.WriteLine("IMEI Received: " + dataReceived);

                    //writing to client answer
                    Console.WriteLine("Sending back answer 01 ");
                    byte[] acception = BitConverter.GetBytes(1);
                    nwStream.Write(acception, 0, 1);

                    //fourzerobytes
                    int fourBytesRecieved = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                    Console.WriteLine(fourBytesRecieved);

                    //getting data array

                    int dataarray = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                    Console.WriteLine(dataarray);

                    //getting crc
                    int crc = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                    var crcRecieved = BitConverter.ToInt16(buffer, 0);
                    Console.WriteLine(crcRecieved);


                    //var avlDataCrcBuffer = new byte[2];
                    //var avlDataCrcBytesTotal = await networkStream.ReadAsync(avlDataCrcBuffer, 0, avlDataCrcBuffer.Length);
                    //rbr = new ReverseBinaryReader(new MemoryStream(avlDataCrcBuffer));
                    //var crcReceived = rbr.ReadUInt16();



                }
            }
        }
    }
}
