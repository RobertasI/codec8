using FMEmulator;

namespace Client
{
    public class AVLPacket
    {
        FMDataSimulator fmdataSimulator = new FMDataSimulator();
        RandomImeiGenerator randomImeiGenerator = new RandomImeiGenerator();
        CrcCalculator crcCalculator = new CrcCalculator();
        public byte[] Imei;
        public byte[] dataArray;
        public byte[] fourZeroBytes;
        public int dataArrayLenght;
        public int CRC;
        public byte[] CRCBytes;

        public AVLPacket()
        {
            fourZeroBytes = new byte[] { 0, 0, 0, 0 };
            dataArray = fmdataSimulator.GenerateAVLData();
            Imei = randomImeiGenerator.GenerateRandomImeiBytes();
            dataArrayLenght = dataArray.Length;
            CRC = crcCalculator.ComputeChecksum(dataArray);
            CRCBytes = crcCalculator.ComputeChecksumBytes(dataArray);
        }
                     
    }
}
