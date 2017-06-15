using Client;
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
    class ClientHandler
    {
        
        static string categoryName = "Clients";
        static string counterName = "Clients online";
        PerformanceCounter clientsCounter = new PerformanceCounter(categoryName, counterName);
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public async void acceptClients(TcpListener listener)
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                handleClientData(client);
            }
        }

        public async Task handleClientData(TcpClient client)
        {
            //int clientsCounter;
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

                //getting data array lenght
                byte[] AvlDataArrayLenghtBuffer = new byte[4];
                int dataArrayLenght = await nwStream.ReadAsync(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                int iDataArrayLenght = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);

                //getting crc
                byte[] crcBuffer = new byte[2];
                int crc = await nwStream.ReadAsync(crcBuffer, 0, crcBuffer.Length);
                var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);

                //getting data array
                byte[] AvlDataArrayBuffer = new byte[iDataArrayLenght];
                int dataarray = await nwStream.ReadAsync(AvlDataArrayBuffer, 0, iDataArrayLenght);
                DataDecoder datadecoder = new DataDecoder();
                var AvlData = datadecoder.Decode(AvlDataArrayBuffer);


                for (int i = 0; i < AvlData.Count; i++)
                {
                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                    long cdrTimestamp = (long)AvlData[1];
                    serverLog.Date = epochStart.AddMilliseconds(cdrTimestamp);

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
                    
                    clientsCounter.Decrement();
                    Logger.Info("Clients online: " + clientsCounter.NextValue().ToString());
                }

                else
                {
                    // resend data


                }
            }
        }
    }
}
