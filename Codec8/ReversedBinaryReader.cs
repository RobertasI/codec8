using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codec8
{
    public class ReversedBinaryReader : BinaryReader
    {

        public ReversedBinaryReader(Stream stream)  : base(stream)
        {

        }


        public override int ReadInt32()
        {
            byte[] a32 = base.ReadBytes(4);
            Array.Reverse(a32);
            return BitConverter.ToInt32(a32, 0);
        }

        public override short ReadInt16()
        {
            byte[] a16 = base.ReadBytes(2);
            Array.Reverse(a16);
            return BitConverter.ToInt16(a16, 0);
        }


        public override long ReadInt64()
        {
            byte[] a64 = base.ReadBytes(8);
            Array.Reverse(a64);
            return BitConverter.ToInt64(a64, 0);
        }
    }
}
