using System;
using System.Linq;

namespace FMEmulator
{
    public class RandomImeiGenerator
    {
        public byte[] GenerateRandomImeiBytes()
        {
            Random random = new Random();
            var imei = random.Next(100000000, 999999999).ToString("X16");

            return Enumerable.Range(0, imei.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(imei.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
