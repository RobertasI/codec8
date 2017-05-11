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
           
        }
        byte[] byteArray = StringConverter.StringToByteArray();
        Stream stream = new MemoryStream(StringConverter.StringToByteArray());
        ReversedBinaryReader reversedbinary = new ReversedBinaryReader(new MemoryStream(StringConverter.StringToByteArray()));

        public void getNubmerOfData()
        {
            int numberOfData = reversedbinary.ReadInt16(StringConverter.ReadBytes(byteArray,1,1));
            Console.WriteLine(numberOfData);
            if (numberOfData == null) { }
        }

    }
}
