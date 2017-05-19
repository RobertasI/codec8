using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codec8
{
    class IotElement
    {
        //oneByte = new Dictionary<int, byte>();
        Dictionary<int, short> twoBytes = new Dictionary<int, short>();
        Dictionary<int, int> fourBytes = new Dictionary<int, int>();
        Dictionary<int, long> eightByte = new Dictionary<int, long>();


        public IDictionary<int, byte> oneByte { get; set; }
        public IDictionary<int, int> twoByte { get; set; }

        //public int[] oneByteElementId { get; set; }
        //public int[] oneByteElementValue { get; set; }
        //public int[] twoBytesElementId { get; set; }
        //public int[] twoBytesElementValue { get; set; }
        //public int[] fourBytesElementId { get; set; }
        //public int[] fourBytesElementValue { get; set; }
        //public int[] eightBytesElementId { get; set; }
        //public int[] eightBytesElementValue { get; set; }

    }
}
