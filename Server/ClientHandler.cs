using Client;
using Codec8;
using Server.DataAccess;
using Server.Domain;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler
    {
        public int clientsCounter;


        public async void acceptClients(TcpListener listener)
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                handleClients(client);
            }
        }

        public async Task handleClients(TcpClient client)
        {
            //int clientsCounter;
            ServerLog serverLog = new ServerLog();

            ServerLogDataService serverLogDataService = new ServerLogDataService();

            using (NetworkStream nwStream = client.GetStream())
            {
                //recieving IMEI
                byte[] imeiBuffer = new byte[8];
                var bytesCount = nwStream.ReadAsync(imeiBuffer, 0, imeiBuffer.Length);
                serverLog.Imei = BitConverter.ToInt64(imeiBuffer, 0);
                Console.WriteLine("IMEI Received: " + serverLog.Imei);
                clientsCounter++;
                Console.WriteLine("Clients online: " + clientsCounter);
                
                //writing answer to client
                Console.WriteLine("Sending back answer 01 ");
                byte[] acception = BitConverter.GetBytes(1);
                await nwStream.WriteAsync(acception, 0, 1);

                //fourzerobytes
                byte[] zeroBytesBuffer = new byte[4];
                int fourBytesRecieved = await nwStream.ReadAsync(zeroBytesBuffer, 0, zeroBytesBuffer.Length);
                Console.WriteLine("Zero bytes: " + fourBytesRecieved);

                //getting data array lenght
                byte[] AvlDataArrayLenghtBuffer = new byte[4];
                int dataArrayLenght = await nwStream.ReadAsync(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                int iDataArrayLenght = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                Console.WriteLine("Data lenght as int: " + dataArrayLenght);

                //getting crc
                byte[] crcBuffer = new byte[2];
                int crc = await nwStream.ReadAsync(crcBuffer, 0, crcBuffer.Length);
                var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                Console.WriteLine("CRC recieved:" + crcRecieved);

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

                    Console.WriteLine(AvlData[i]);
                }

                //calculating  crc
                CrcCalculator crccalculator = new CrcCalculator();
                var crcCalculated = crccalculator.ComputeChecksum(AvlDataArrayBuffer);
                Console.WriteLine("CRC calculated:" + crcCalculated);


                if (crcRecieved == crcCalculated)
                {
                    //add data to serverlog 
                    Console.WriteLine("crc matches");

                    serverLogDataService.Add(serverLog);
                    clientsCounter--;
                    Console.WriteLine("Clients online: " + clientsCounter);
                }

                else
                {
                    // resend data


                }
            }
        }

    }
}
