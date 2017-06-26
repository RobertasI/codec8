using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Codec8
{
    public class DataEncoder
    {
        public byte[] Encode(List<string> datalist)
        {
            var encodedToString = new StringBuilder();

            foreach (var item in datalist)
            {
                encodedToString.Append(item.ToString());
            }

            var stringData = encodedToString.ToString();
            Console.WriteLine(stringData);

            //var lenght = stringer.Length;

            //byte[] toBytes = Encoding.ASCII.GetBytes(stringer);
            //return toBytes;

            //return Enumerable.Range(0, stringData.Length)
            //         .Where(x => x % 2 == 0)
            //         .Select(x => Convert.ToByte(stringData.Substring(x, 2), 16))
            //         .ToArray(); ;

            //return Enumerable.Range(0, hex.Length)
            //                 .Where(x => x % 2 == 0)
            //                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            //                 .ToArray();

           
            return Enumerable.Range(0, stringData.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Byte.Parse(stringData.Substring(x, 2), NumberStyles.HexNumber))
                .ToArray();
       

    }
    }
}
