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
            oneByte = new Dictionary<int, int>();
            twoBytes = new Dictionary<int, int>();
            fourBytes = new Dictionary<int, int>();
            eightBytes = new Dictionary<int, long>();
        }



        public IDictionary<int, int> oneByte { get; set; }
        public IDictionary<int, int> twoBytes { get; set; }
        public IDictionary<int, int> fourBytes { get; set; }
        public IDictionary<int, long> eightBytes { get; set; }
    }
}
