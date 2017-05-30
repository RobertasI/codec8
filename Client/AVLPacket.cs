using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class AVLPacket
    {
        public byte[] fourZeroBytes = new byte[] {0,0,0,0};
        public int dataLenght { get; set; }
        public byte[] dataArray { get; set; }
        public int CRC { get; set; }
    }
}
