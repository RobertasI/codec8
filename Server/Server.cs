using Codec8;
using System;
using System.Net;
using System.Net.Sockets;
using Client;
using NLog;
using System.Threading.Tasks;
using Server.Domain;
using Server.DataAccess;

namespace Server
{
    class Server
    {
        private Logger ConsoleLogger = LogManager.GetLogger("consoleLogger");
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        private int clientsCounter;

        public static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer();
            Console.ReadLine();
        }

        public async Task StartServer()
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
            //ServerLogContext serverlogcontext = new ServerLogContext();

            //var testServerLog = new ServerLog
            //{
            //    Imei = 1256
            //};
            //serverLogDataServie.Add(testServerLog);

            using (NetworkStream nwStream = client.GetStream())
            {
                //recieving IMEI
                byte[] imeiBuffer = new byte[8];
                var bytesCount = nwStream.ReadAsync(imeiBuffer, 0, imeiBuffer.Length);
                serverLog.Imei = BitConverter.ToInt64(imeiBuffer, 0);
                Console.WriteLine("IMEI Received: " + serverLog.Imei);
                ConsoleLogger.Info("IMEI Received: " + serverLog.Imei);
                clientsCounter++;
                Console.WriteLine("Clients online: " + clientsCounter);


                //serverLogDataServie.Add(serverLog);
                //serverlogcontext.ServerLog.Add(serverLog);
                //await serverlogcontext.SaveChangesAsync();
                //writing to client answer
                ConsoleLogger.Info("Sending back answer 01 ");
                Console.WriteLine("Sending back answer 01 ");
                byte[] acception = BitConverter.GetBytes(1);
                await nwStream.WriteAsync(acception, 0, 1);

                //fourzerobytes
                byte[] zeroBytesBuffer = new byte[4];
                int fourBytesRecieved = await nwStream.ReadAsync(zeroBytesBuffer, 0, zeroBytesBuffer.Length);
                ConsoleLogger.Info("zero bytes: " + fourBytesRecieved);
                Console.WriteLine("Zero bytes: " + fourBytesRecieved);

                //getting data array lenght
                byte[] AvlDataArrayLenghtBuffer = new byte[4];
                int dataArrayLenght = await nwStream.ReadAsync(AvlDataArrayLenghtBuffer, 0, AvlDataArrayLenghtBuffer.Length);
                int iDataArray = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0);
                var AvlHeader = BitConverter.ToInt16(AvlDataArrayLenghtBuffer, 0); //will need to change this 
                ConsoleLogger.Info("Data lenght as int: " + AvlHeader);
                Console.WriteLine("Data lenght as int: " + AvlHeader);

                //getting crc

                byte[] crcBuffer = new byte[2];
                int crc = await nwStream.ReadAsync(crcBuffer, 0, crcBuffer.Length);
                var crcRecieved = BitConverter.ToInt16(crcBuffer, 0);
                ConsoleLogger.Info("CRC recieved:" + crcRecieved);
                Console.WriteLine("CRC recieved:" + crcRecieved);

                //getting data array
                byte[] AvlDataArrayBuffer = new byte[iDataArray];
                int dataarray = await nwStream.ReadAsync(AvlDataArrayBuffer, 0, iDataArray);
                DataDecoder datadecoder = new DataDecoder();
                var AvlData = datadecoder.Decode(AvlDataArrayBuffer);
                foreach (var item in AvlData)
                {
                    Console.WriteLine(item);
                }

                //calculating  crc
                CrcCalculator crccalculator = new CrcCalculator();
                var crcCalculated = crccalculator.ComputeChecksum(AvlDataArrayBuffer);
                ConsoleLogger.Info("CRC calculated:" + crcCalculated);
                Console.WriteLine("CRC calculated:" + crcCalculated);


                if (crcRecieved == crcCalculated)
                {
                    //add data to serverlog 
                    Console.WriteLine("crc matches");

                    clientsCounter--;
                    Console.WriteLine("Clients online: " + clientsCounter);

                }
                else
                {
                    // resend data
                    //create new method for data sending
                    //ClientHandler, DataHendler?

                }

            }

        }
    }
}
