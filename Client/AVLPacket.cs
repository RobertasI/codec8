using FMEmulator;
using System;
using System.Linq;

namespace Client
{
    public class AVLPacket
    {
        FMDataSimulator fmdataSimulator = new FMDataSimulator();
        CrcCalculator crcCalculator = new CrcCalculator();
        public byte[] Imei;
        public byte[] dataArray;
        public byte[] fourZeroBytes;
        public byte[] dataArrayLenght;
        public int CRC;
        public byte[] CRCBytes;
        public byte[] AvlDataHeader;

        public AVLPacket()
        {
            fourZeroBytes = new byte[] { 0, 0, 0, 0 };
            dataArray = fmdataSimulator.GenerateAVLData();
            dataArrayLenght = BitConverter.GetBytes(dataArray.Length);
            CRC = crcCalculator.ComputeChecksum(dataArray);
            CRCBytes = crcCalculator.ComputeChecksumBytes(dataArray);
            AvlDataHeader = fourZeroBytes.Concat(dataArrayLenght).ToArray();
        }
    }
}
