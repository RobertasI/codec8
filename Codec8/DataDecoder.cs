using Codec8;
using System;
using System.IO;
using System.Linq;

namespace Codec8
{
    class DataDecoder
    {
        public static void Main()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2),0);
            Console.WriteLine(numberOfData);
        }
        byte[] byteArray = StringConverter.StringToByteArray();
        Stream stream = new MemoryStream(StringConverter.StringToByteArray());
        ReversedBinaryReader reversedbinary = new ReversedBinaryReader(new MemoryStream(StringConverter.StringToByteArray()));

       // var numberOfRecords = Convert.ToInt16(hexBytes.ReadBytes(_currentByte, 1)[0]);


        public void getNubmerOfData()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2), 0);
            Console.WriteLine(numberOfData);
          
        }

    }
}
