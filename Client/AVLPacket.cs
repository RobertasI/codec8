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
        public int dataArrayLenght = 254;
        public byte[] dataArray = new byte[] { 1, 2, 3, 2, 4, 56, 5, 2, 3, 5, 2 };
        public int CRC { get; set; }
    }
}
