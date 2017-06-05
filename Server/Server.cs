using Codec8;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Client;
using System.IO;
using Server.DataAccess;
using Server.Domain;
using NLog;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private static readonly Logger ConsoleLogger = LogManager.GetLogger("consoleLogger");

        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        public static void Main(string[] args)
        {


        }

        public async Task StartServer()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            listener.Start();
            ConsoleLogger.Info("Listening...");


            while(true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                await handleClients(client);
            }

        }

        public async Task handleClients(TcpClient client)  
        { 

                 
                using (NetworkStream nwStream = client.GetStream())
                {
                    ServerDataService serverdataservice = new ServerDataService();
                    ServerData serverData = new ServerData();

                    //recieving IMEI
                    byte[] imeiBuffer = new byte[8];
                    var bytesCount = nwStream.Read(imeiBuffer, 0, imeiBuffer.Length);
                    serverData.Imei = BitConverter.ToInt64(imeiBuffer, 0);
                    ConsoleLogger.Info("IMEI Received: " + serverData.Imei);

                    //writing to client answer
                    ConsoleLogger.Info("Sending back answer 01 ");
                    byte[] acception = BitConverter.GetBytes(1);
                    nwStream.Write(acception, 0, 1);

                    //fourzerobytes
                    byte[] zeroBytesBuffer = new byte[4];
                    int fourBytesRecieved = nwStream.Read(zeroBytesBuffer, 0, zeroBytesBuffer.Length);
                    ConsoleLogger.Info("zero bytes: " + fourBytesRecieved);

                    //getting data array lenght
                    byte[] AvlDataArrayLenghtBuffer = new byte[4];
                    int dataArrayLenght = nwStream.Read(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                    int iDataArray = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                    serverData.AvlHeader = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0); //will need to change this 
                    ConsoleLogger.Info("Data lenght as int: " + serverData.AvlHeader);


                    //getting crc

                    byte[] crcBuffer = new byte[2];
                    int crc = nwStream.Read(crcBuffer, 0, crcBuffer.Length);
                    var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                    ConsoleLogger.Info("CRC recieved:" + crcRecieved);




                    //getting data array
                    byte[] AvlDataArrayBuffer = new byte[iDataArray];
                    int dataarray = nwStream.Read(AvlDataArrayBuffer, 0, iDataArray);
                    DataDecoder datadecoder = new DataDecoder();
                    serverData.AvlData = datadecoder.Decode(AvlDataArrayBuffer);
                    foreach (var item in serverData.AvlData)
                    {
                        ConsoleLogger.Info(item);
                    }

                    //calculating  crc
                    CrcCalculator crccalculator = new CrcCalculator();
                    var crcCalculated = crccalculator.ComputeChecksum(AvlDataArrayBuffer);
                    ConsoleLogger.Info("CRC calculated:" + crcCalculated);

                }
        }

    }
}
