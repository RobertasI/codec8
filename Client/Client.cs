using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            //---data to send to the server---
            //string textToSend = DateTime.Now.ToString();
            long Imei = 123456789012345;
            byte[] ImeiAsBytes = BitConverter.GetBytes(Imei);


            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();

            Console.WriteLine("Sending IMEI : " + Imei);
            nwStream.Write(ImeiAsBytes, 0, ImeiAsBytes.Length);


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            short answerToImei = BitConverter.ToInt16(bytesToRead, 0);

            if (answerToImei == 1)
            {
                Console.WriteLine("You are accepted");

                AVLPacket avlpacket = new AVLPacket();
                nwStream.Write(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);

                
                //sending dataarray
                nwStream.Write(avlpacket.dataArray, 0, avlpacket.dataArray.Length);


                //sending crc
                CrcCalculator crccalculator = new CrcCalculator();
                nwStream.Write(crccalculator.ComputeChecksumBytes(avlpacket.dataArray), 0, 2);


            }
            else { Console.WriteLine("You are not accepted"); }

   
        }
    }
}
