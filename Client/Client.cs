using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading.Tasks;
using log4net;

namespace Client
{
    public class Client
    {

        private int PORT_NO = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
        private string SERVER_IP = ConfigurationManager.AppSettings["ip"];
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            for (int i = 0; i < 7; i++)
            {
                Client client = new Client();
                Task task = new Task(client.StartClient);
                task.Start();
            }

            Console.ReadLine();
        }

        public async void StartClient()
        {
            
            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient();
            await client.ConnectAsync(SERVER_IP, PORT_NO);

            SendData(client);
            
        }

        public async Task SendData(TcpClient client)
        {
            NetworkStream nwStream = client.GetStream();

            //---data to send to the server---
            long Imei = 123456789012345;
            byte[] ImeiAsBytes = BitConverter.GetBytes(Imei);

            Logger.Info("Sending IMEI : " + Imei);
            await nwStream.WriteAsync(ImeiAsBytes, 0, ImeiAsBytes.Length);

            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);
            short answerToImei = BitConverter.ToInt16(bytesToRead, 0);
            Logger.Info("Getting answer: " + answerToImei);
            if (answerToImei == 1)
            {
                Logger.Info("You are accepted");

                AVLPacket avlpacket = new AVLPacket();

                //sending zero bytes
                await nwStream.WriteAsync(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);
                string sFourzeros = System.Text.Encoding.UTF8.GetString(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);

                //sending datalenght
                avlpacket.dataArrayLenght = avlpacket.dataArray.Length;
                await nwStream.WriteAsync(BitConverter.GetBytes(avlpacket.dataArrayLenght), 0, 4);

                //sending crc first, because otherwise it doesnt work
                CrcCalculator crccalculator = new CrcCalculator();
                int crc = crccalculator.ComputeChecksum(avlpacket.dataArray);
                await nwStream.WriteAsync(crccalculator.ComputeChecksumBytes(avlpacket.dataArray), 0, 2);

                //sending dataarray
                await nwStream.WriteAsync(avlpacket.dataArray, 0, avlpacket.dataArray.Length);

                Logger.Info("Sending zero bytes : " + sFourzeros + "---" + " Crc : " + crc);

            }
            else { Logger.Info("You are not accepted"); }
        }
    }
}
