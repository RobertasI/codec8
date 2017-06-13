using Codec8;
using System;
using System.Net;
using System.Net.Sockets;
using Client;
using NLog;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccess;
using System.Configuration;

namespace Server
{
    class Server
    {
        public Logger ConsoleLogger = LogManager.GetLogger("consoleLogger");
        //var ip = IPAddress.Parse(ConfigurationManager.AppSettings["serverIp"]);
        //var port = Convert.ToInt32(ConfigurationManager.AppSettings["serverPort"]);
        int PORT_NO = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
        string SERVER_IP = ConfigurationManager.AppSettings["ip"]; //"127.0.0.1";
        private int clientsCounter;

        public static void Main(string[] args)
        {
 
            Server server = new Server();
            server.StartServer();
            Console.ReadLine();
        }

        public void StartServer()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            listener.Start();
            Console.WriteLine("Listening... ");
            ConsoleLogger.Info("Listening...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                handleClients(client);
            }

        }

        public async Task handleClients(TcpClient client)
        {
            ServerLog serverLog = new ServerLog();

            ServerLogDataService serverLogDataServie = new ServerLogDataService();
      

            using (NetworkStream nwStream = client.GetStream())
            {

                //recieving IMEI
                byte[] imeiBuffer = new byte[8];
                var bytesCount = nwStream.ReadAsync(imeiBuffer, 0, imeiBuffer.Length);
                serverLog.Imei = BitConverter.ToInt64(imeiBuffer, 0);
                Console.WriteLine("IMEI Received: " + serverLog.Imei);
                clientsCounter++;
                Console.WriteLine("Clients online: " + clientsCounter);
                                
 
                //writing to client answer
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
                int iDataArray = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                var AvlHeader = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0); //will need to change this 
                Console.WriteLine("Data lenght as int: " + AvlHeader);

                //getting crc

                byte[] crcBuffer = new byte[2];
                int crc = await nwStream.ReadAsync(crcBuffer, 0, crcBuffer.Length);
                var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                Console.WriteLine("CRC recieved:" + crcRecieved);

                //getting data array
                byte[] AvlDataArrayBuffer = new byte[iDataArray];
                int dataarray = await nwStream.ReadAsync(AvlDataArrayBuffer, 0, iDataArray);
                DataDecoder datadecoder = new DataDecoder();
                var AvlData = datadecoder.Decode(AvlDataArrayBuffer);

                for (int i = 0; i < AvlData.Count; i++)
                //foreach (var item in AvlData)
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

                    serverLogDataServie.Add(serverLog);
                    clientsCounter--;
                    Console.WriteLine("Clients online: " + clientsCounter);

                }
                else
                {
                    // resend data
                    //create new method for data sending?
                    //ClientHandler, DataHendler?

                }

            }

        }
    }
}
