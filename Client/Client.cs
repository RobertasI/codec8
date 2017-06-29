using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading.Tasks;
using log4net;
using Codec8;

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

            for (int i = 0; i < 10; i++)
            {
                TcpClient tcpClient = new TcpClient();
                Client client = new Client();
                tcpClient.Connect(client.SERVER_IP, client.PORT_NO);
                await client.SendData(tcpClient);
            }
        }

        public async Task SendData(TcpClient client)
        {
            NetworkStream nwStream = client.GetStream();
            AVLPacket avlpacket = new AVLPacket();

            //send imei
            await nwStream.WriteAsync(avlpacket.Imei, 0, avlpacket.Imei.Length);
            var imei = BitConverter.ToInt64(avlpacket.Imei, 0);
            Logger.Info("Sending IMEI : " + imei );


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = await nwStream.ReadAsync(bytesToRead, 0, client.ReceiveBufferSize);

            short answerToImei = BitConverter.ToInt16(bytesToRead, 0);
            if (answerToImei == 1)
            {
                Logger.Info("Imei " + imei + " is accepted");

                await nwStream.WriteAsync(avlpacket.AvlDataHeader, 0, avlpacket.AvlDataHeader.Length);

                Console.WriteLine("Data packet lenght: " + avlpacket.AvlDataHeader.Length);

                //sending zero bytes
                //await nwStream.WriteAsync(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);

                //sending datalenght
                //await nwStream.WriteAsync(BitConverter.GetBytes(avlpacket.dataArrayLenght), 0, 4);

                //sending dataarray
                await nwStream.WriteAsync(avlpacket.dataArray, 0, avlpacket.dataArray.Length);

                //sending crc 
                await nwStream.WriteAsync(avlpacket.CRCBytes, 0, 2);


                Logger.Info("Imei " + imei + " data has been sent");
            }
            else { Logger.Info("Imei " + imei + " is not accepted"); }
        }
    }
}
