using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codec8;

namespace Client
{
    class AVLPacket
    {
        public byte[] fourZeroBytes = new byte[] {0,0,0,0};
        public int dataArrayLenght = 254;
        public byte[] dataArray = StringConverter.StringToByteArray();

        public int CRC { get; set; }
    }
}
