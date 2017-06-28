﻿using Client;
using Codec8;
using Server.DataAccess;
using Server.Domain;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using log4net;

namespace Server
{
    class ClientHandler : IDisposable
    {
        
        static string categoryName = "Clients";
        static string counterName = "Clients online";
        PerformanceCounter clientsCounter = new PerformanceCounter(categoryName, counterName);
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public async void acceptClients(TcpListener listener)
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                handleClientData(client);
            }
        }

        public async Task handleClientData(TcpClient client)
        {
            clientsCounter.ReadOnly = false;
            ServerLog serverLog = new ServerLog();

            ServerLogDataService serverLogDataService = new ServerLogDataService();

            using (NetworkStream nwStream = client.GetStream())
            {
                //recieving IMEI
                byte[] imeiBuffer = new byte[8];
                var bytesCount = nwStream.ReadAsync(imeiBuffer, 0, imeiBuffer.Length);
                serverLog.Imei = BitConverter.ToInt64(imeiBuffer, 0);
                Logger.Info("IMEI Received: " + serverLog.Imei);
                
                //writing answer to client, always 1 to send
                byte[] acception = BitConverter.GetBytes(1);
                await nwStream.WriteAsync(acception, 0, 1);

                //add accepted client and print it in console and file
                clientsCounter.Increment();
                Logger.Info("Clients online: " + clientsCounter.NextValue().ToString());

                //reading zero bytes
                byte[] zeroBytesBuffer = new byte[4];
                int fourBytesRecieved = await nwStream.ReadAsync(zeroBytesBuffer, 0, zeroBytesBuffer.Length);
                Console.WriteLine("zero bytes read");
                //getting data array lenght
                byte[] AvlDataArrayLenghtBuffer = new byte[4];
                int dataArrayLenght = await nwStream.ReadAsync(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                int iDataArrayLenght = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                Console.WriteLine("datalenght read");

                //getting crc
                byte[] crcBuffer = new byte[2];
                int crc = await nwStream.ReadAsync(crcBuffer, 0, crcBuffer.Length);
                var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                Console.WriteLine("crc read");

                //getting data array
                byte[] AvlDataArrayBuffer = new byte[iDataArrayLenght];
                int dataarray = await nwStream.ReadAsync(AvlDataArrayBuffer, 0, iDataArrayLenght);
                Console.WriteLine("dataarray got");
                DataDecoder datadecoder = new DataDecoder();
                var AvlData = datadecoder.Decode(AvlDataArrayBuffer);


                for (int i = 0; i < AvlData.Count; i++)
                {

                    serverLog.Date = (DateTime)AvlData[1];


                    serverLog.Longitude = (int)AvlData[3];
                    serverLog.Latitude = (int)AvlData[4];
                }

                //calculating  crc
                CrcCalculator crccalculator = new CrcCalculator();
                var crcCalculated = crccalculator.ComputeChecksum(AvlDataArrayBuffer);


                if (crcRecieved == crcCalculated)
                {
                    //add data to serverlog 
                    Console.WriteLine("crc matches");

                    //write needed data to console and file 
                    Logger.Info("Imei: " + serverLog.Imei + "---" + " Date: " + serverLog.Date + "---" + " Longitude: " + serverLog.Longitude 
                        + "---" + " Latitude: " + serverLog.Latitude);
                    //add data to database
                    serverLogDataService.Add(serverLog);
                    client.Client.Disconnect(false);
                    clientsCounter.Decrement();
                    Logger.Info("Clients online: " + clientsCounter.NextValue().ToString());
                }

                else
                {
                    // resend data
                    Console.WriteLine("crc doesnt match");
                    Client.Client clientObject = new Client.Client();
                    await clientObject.SendData(client);
                }
            }
        }

        public void Dispose()
        {
            clientsCounter.Dispose();
        }
    }
}
