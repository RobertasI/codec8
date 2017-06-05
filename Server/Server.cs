using Codec8;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Client;
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
                    byte[] imeiBuffer = new byte[8];
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
                    Console.WriteLine("zero bytes: " + fourBytesRecieved);

                    //getting data array lenght
                    byte[] AvlDataArrayLenghtBuffer = new byte[4];
                    int dataArrayLenght = nwStream.Read(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                    int iDataArray = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                    Console.WriteLine("Data lenght as int: " + iDataArray);

                    //getting crc

                    byte[] crcBuffer = new byte[2];
                    int crc = nwStream.Read(crcBuffer, 0, crcBuffer.Length);
                    var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                    Console.WriteLine("CRC recieved:" + crcRecieved);

                    


                    //getting data array
                    byte[] AvlDataArrayBuffer = new byte[iDataArray];
                    int dataarray = nwStream.Read(AvlDataArrayBuffer, 0, iDataArray);
                    DataDecoder datadecoder = new DataDecoder();
                    ArrayList decodedDataArray = datadecoder.Decode(AvlDataArrayBuffer);
                    foreach (var item in decodedDataArray)
                    {
                        Console.WriteLine(item);
                    }

                    //calculating  crc
                    CrcCalculator crccalculator = new CrcCalculator();
                    var crcCalculated = crccalculator.ComputeChecksum(AvlDataArrayBuffer);
                    Console.WriteLine("CRC calculated:" + crcCalculated);

                }
            }
        }
    }
}
