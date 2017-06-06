using System;
using System.Net.Sockets;
using NLog;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {

        private static readonly Logger ConsoleLogger = LogManager.GetLogger("consoleLogger");

        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";


        static void Main(string[] args)
        {
            Client client = new Client();
            client.StartClient();
            //Task.WaitAll();
        }

        public async Task StartClient()
        {
            
            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient();
            //await client.ConnectAsync(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();

            //---data to send to the server---
            //string textToSend = DateTime.Now.ToString();
            long Imei = 123456789012345;
            byte[] ImeiAsBytes = BitConverter.GetBytes(Imei);


            ConsoleLogger.Info("Sending IMEI : " + Imei);
            Console.WriteLine("aa");
            await nwStream.WriteAsync(ImeiAsBytes, 0, ImeiAsBytes.Length);
            ConsoleLogger.Info("Imei lenght: " + ImeiAsBytes.Length);


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
            short answerToImei = BitConverter.ToInt16(bytesToRead, 0);

            if (answerToImei == 1)
            {
                ConsoleLogger.Info("You are accepted");

                AVLPacket avlpacket = new AVLPacket();

                //sending zero bytes
                await nwStream.WriteAsync(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);
                int iZeroBytes = BitConverter.ToInt32(avlpacket.fourZeroBytes, 0);
                string sFourzeros = System.Text.Encoding.UTF8.GetString(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);
                ConsoleLogger.Info("Sending zero bytes as integer: " + iZeroBytes);
                ConsoleLogger.Info("Zero bytes lenght: " + avlpacket.fourZeroBytes.Length);


                //sending datalenght
                avlpacket.dataArrayLenght = avlpacket.dataArray.Length;
                await nwStream.WriteAsync(BitConverter.GetBytes(avlpacket.dataArrayLenght), 0, 4);
                ConsoleLogger.Info("Sending data lenght: " + avlpacket.dataArray.Length);

                // first sending crc, because otherwise doesnt work
                //sending crc
                CrcCalculator crccalculator = new CrcCalculator();
                int crc = crccalculator.ComputeChecksum(avlpacket.dataArray);
                await nwStream.WriteAsync(crccalculator.ComputeChecksumBytes(avlpacket.dataArray), 0, 2);
                ConsoleLogger.Info("Sending CRC: " + crc);


                //sending dataarray
                await nwStream.WriteAsync(avlpacket.dataArray, 0, avlpacket.dataArray.Length);

            }
            else { ConsoleLogger.Info("You are not accepted"); }
        }
    }
}
