using System;
using System.Linq;
using System.Text;

namespace FMEmulator
{
    public class RandomImeiGenerator
    {

        public byte[] GenerateRandomImeiBytes()
        {
            var imeiStringBuilder = new StringBuilder();
            Random random = new Random();
            for(int i = 0; i < 8; i++)
            {
                imeiStringBuilder.Append(random.Next(10, 99).ToString("X2"));
            }

            var imeiString = imeiStringBuilder.ToString();

            return Enumerable.Range(0, imeiString.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(imeiString.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
