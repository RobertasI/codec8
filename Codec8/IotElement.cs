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
        public IotElement()
        {
            oneByte = new Dictionary<byte, byte>();
            twoBytes = new Dictionary<byte, int>();
            fourBytes = new Dictionary<byte, int>();
            eightBytes = new Dictionary<byte, long>();
        }



        public IDictionary<byte, byte> oneByte { get; set; }
        public IDictionary<byte, int> twoBytes { get; set; }
        public IDictionary<byte, int> fourBytes { get; set; }
        public IDictionary<byte, long> eightBytes { get; set; }
        public int EventID { get; set; }
        public int NumberOfElements { get; set; }
        public byte numberOfOneByteElements { get; set; }
        public byte numberOfTwoByteElements { get; set; }
        public byte numberOfFourByteElements { get; set; }
        public byte numberOfEightByteElements { get; set; }
    }
}
