using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Codec8
{
    public class DataEncoder
    {
        public byte[] Encode(ArrayList datalist)
        {
            var encodedToString = new StringBuilder();

            foreach (var item in datalist)
            {
                encodedToString.Append(item.ToString());
            }

            var stringer = encodedToString.ToString();
            Console.WriteLine(stringer);

            //var lenght = stringer.Length;

            //byte[] toBytes = Encoding.ASCII.GetBytes(stringer);
            //return toBytes;

            return Enumerable.Range(0, stringer.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(stringer.Substring(x, 2), 16))
                     .ToArray(); ;

            //return Enumerable.Range(0, hex.Length)
            //                 .Where(x => x % 2 == 0)
            //                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            //                 .ToArray();
        }
    }
}
