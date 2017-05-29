using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codec8
{
    class DataEncoder
    {
        public byte[] Encode(ArrayList datalist)
        {
            var encodedToString = new StringBuilder();

            foreach (var item in datalist )
            {
                encodedToString.Append(item.ToString());
            }

            return Enumerable.Range(0, (encodedToString.ToString()).Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte((encodedToString.ToString()).Substring(x, 2), 16))
                     .ToArray(); ;
        }
    }
}
