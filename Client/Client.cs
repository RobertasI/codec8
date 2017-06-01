using System;
using System.Net.Sockets;
using Codec8;
using System.IO;


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

                //sending zero bytes
                nwStream.Write(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);
                int iZeroBytes = BitConverter.ToInt32(avlpacket.fourZeroBytes, 0);
                string sFourzeros = System.Text.Encoding.UTF8.GetString(avlpacket.fourZeroBytes, 0, avlpacket.fourZeroBytes.Length);
                Console.WriteLine("Sending zero bytes as integer: " + iZeroBytes);

                //sending datalenght
                nwStream.Write(BitConverter.GetBytes(avlpacket.dataArrayLenght),0, 4);
                Console.WriteLine("Sending data lenght: " + avlpacket.dataArrayLenght);

                //sending dataarray
                nwStream.Write(avlpacket.dataArray, 0, avlpacket.dataArray.Length);
                string sDataArray = System.Text.Encoding.UTF8.GetString(avlpacket.dataArray, 0, avlpacket.dataArray.Length);
                string stringDataArray = avlpacket.dataArray.ToString();
                ReversedBinaryReader reversedbinaryreader = new ReversedBinaryReader(new MemoryStream(avlpacket.dataArray));
                long longDataArray = reversedbinaryreader.ReadInt64();
                int iDataArray = BitConverter.ToInt32(avlpacket.dataArray, 0);
                Console.WriteLine("Sending data array encoded: " + sDataArray);
                Console.WriteLine("Sending data array tostring: " + stringDataArray);
                Console.WriteLine("Sending data array as int:" + iDataArray);
                Console.WriteLine("Sending data array as long:" + longDataArray);
                    

                //sending crc
                try {
                    CrcCalculator crccalculator = new CrcCalculator();
                    int crc = crccalculator.ComputeChecksum(avlpacket.dataArray);
                    nwStream.Write(crccalculator.ComputeChecksumBytes(avlpacket.dataArray), 0, 2);
                    Console.WriteLine("Sending CRC: " + crc);
                }
                catch
                {
                    

                    Console.WriteLine("dafuq ");
                }
                
            }
            else { Console.WriteLine("You are not accepted"); }

   
        }
    }
}
