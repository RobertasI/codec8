using Codec8;

namespace Client
{
    class AVLPacket
    {
        public byte[] fourZeroBytes = new byte[] {0,0,0,0};
        public byte[] dataArray = StringConverter.StringToByteArray();
        public int dataArrayLenght { get; set; }
        public int CRC { get; set; }
    }
}
