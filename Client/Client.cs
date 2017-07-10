using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading.Tasks;
using log4net;
using System.Threading;
using FMEmulator;
using System.Collections.Generic;

namespace Client
{
    public class Client
    {

        private int PORT_NO = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
        private string SERVER_IP = ConfigurationManager.AppSettings["ip"];
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Client client = new Client();
            client.StartClient();
            Console.ReadLine();
        }

        public async void StartClient()
        {
            //Console.WriteLine("Enter how many clients to create");
            // var clientsWanted = Convert.ToInt32(Console.ReadLine());
            var imeiBytesList = new List<byte[]>();

            for (int i = 0; i < 3; i++)
            {
                RandomImeiGenerator randomImeiGenerator = new RandomImeiGenerator();

                var randomImei = randomImeiGenerator.GenerateRandomImeiBytes();
                imeiBytesList.Add(randomImei);
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            
            for (int i = 0; i < 3; i++)
            {
                Random random  = new Random();
                byte[] randomImeiBytes = imeiBytesList[random.Next(imeiBytesList.Count)];

                TcpClient tcpClient = new TcpClient();
                Client client = new Client();
                tcpClient.Connect(client.SERVER_IP, client.PORT_NO);

                await client.SendData(tcpClient, randomImeiBytes);
                //GC.Collect();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time elapsed: " + elapsedMs);
        }

        public async Task SendData(TcpClient client, byte[] imeiBytes )
        {
            NetworkStream nwStream = client.GetStream();
            AVLPacket avlpacket = new AVLPacket();

            //send imei
            await nwStream.WriteAsync(imeiBytes, 0, avlpacket.Imei.Length);
            var imei = BitConverter.ToInt64(avlpacket.Imei, 0);
            Logger.Info("Sending IMEI : " + imei);


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);

            short answerToImei = BitConverter.ToInt16(bytesToRead, 0);

            if (answerToImei == 1)
            {

                Logger.Info("Imei " + imei + " is accepted");


                await nwStream.WriteAsync(avlpacket.AvlDataHeader, 0, avlpacket.AvlDataHeader.Length);

                //sending dataarray
                await nwStream.WriteAsync(avlpacket.dataArray, 0, avlpacket.dataArray.Length);

                //sending crc 
                await nwStream.WriteAsync(avlpacket.CRCBytes, 0, 2);

                Logger.Info("Imei " + imei + " data has been sent");
                //Thread.Sleep(2000);

            }
            else { Logger.Info("Imei " + imei + " is not accepted"); }
        }
    }
}
