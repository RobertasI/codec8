using System;
using System.Linq;

namespace Codec8Decoder
{
    class DataDecoder
    {
        string stream = Console.ReadLine();

        public static byte[] StringToByteArray(string stream)
        {
            return Enumerable.Range(0, stream.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(stream.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
