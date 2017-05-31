using System;
using System.Net;
using System.Net.Sockets;
using Codec8;
using System.IO;

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
                    //recieving IMEI
                    byte[] imeiBuffer = new byte[15];
                    var bytesCount = nwStream.Read(imeiBuffer, 0, imeiBuffer.Length);
                    long dataReceived = BitConverter.ToInt64(imeiBuffer, 0);
                    Console.WriteLine(bytesCount);
                    Console.WriteLine("IMEI Received: " + dataReceived);

                    //writing to client answer
                    Console.WriteLine("Sending back answer 01 ");
                    byte[] acception = BitConverter.GetBytes(1);
                    nwStream.Write(acception, 0, 1);

                    //fourzerobytes
                    byte[] zeroBytesBuffer = new byte[4];
                    int fourBytesRecieved = nwStream.Read(zeroBytesBuffer, 0, zeroBytesBuffer.Length);
                    Console.WriteLine(fourBytesRecieved);

                    //getting data array lenght
                    byte[] dataArrayLenghtBuffer = new byte[4];
                    ReversedBinaryReader reversedbinaryreader = new ReversedBinaryReader(new MemoryStream(dataArrayLenghtBuffer));
                    int dataArrayLenght = reversedbinaryreader.ReadInt16();
                    Console.WriteLine("Data lenght:" + dataArrayLenght);

                    //getting data array
                    byte[] AvlDataArrayBuffer = new byte[dataArrayLenght];
                    int dataarray = nwStream.Read(AvlDataArrayBuffer, 0, AvlDataArrayBuffer.Length);
                    Console.WriteLine(dataarray);

                    //getting crc
                    byte[] crcBuffer = new byte[2];
                    int crc = nwStream.Read(crcBuffer, 0, crcBuffer.Length);
                    var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
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
