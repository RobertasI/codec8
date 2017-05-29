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


        public int EventID { get; set; }
        public int NumberOfElements { get; set; }
        public byte numberOfOneByteElements { get; set; }
        public byte numberOfTwoByteElements { get; set; }
        public byte numberOfFourByteElements { get; set; }
        public byte numberOfEightByteElements { get; set; }
        public byte oneByteID { get; set; }
        public byte twoBytesID { get; set; }
        public byte fourBytesID { get; set; }
        public byte eightBytesID { get; set; }
        public int oneByteValue { get; set; }
        public int twoBytesValue { get; set; }
        public int fourBytesValue { get; set; }
        public int eightBytesValue { get; set; }
    }
}
