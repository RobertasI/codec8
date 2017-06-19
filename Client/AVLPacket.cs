using FMEmulator;
namespace Client
{
    public class AVLPacket
    {
        FMDataSimulator fmdataSimulator = new FMDataSimulator();
        public byte[] dataArray;

        public AVLPacket()
        {
            dataArray = fmdataSimulator.GenerateAVLData();
        }
               
        public byte[] fourZeroBytes = new byte[] {0,0,0,0};
        public int dataArrayLenght { get; set; }
        public int CRC { get; set; }
    }
}
