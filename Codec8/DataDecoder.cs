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
            var _currentByte = 1;

            int numberOfData = getNumberOfData();
            Console.WriteLine(numberOfData);

            _currentByte += 1;

            for (int i = 0; i < getNumberOfData(); i++)
            {
                var gpsElement = new GpsElement();



                var timeStamp = ReversedBinaryReader.ReadInt64

            }

        }

        //byte[] byteArray = StringConverter.StringToByteArray();
        //Stream stream = new MemoryStream(StringConverter.StringToByteArray());
        //ReversedBinaryReader reversedbinary = new ReversedBinaryReader(new MemoryStream(StringConverter.StringToByteArray()));

       // var numberOfRecords = Convert.ToInt16(hexBytes.ReadBytes(_currentByte, 1)[0]);


        public static int getNumberOfData()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2), 0);
            return numberOfData;
        }

    }

}
