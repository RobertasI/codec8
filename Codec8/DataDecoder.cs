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

            int _currentByte = 1;


            int numberOfData = getNumberOfData();
            Console.WriteLine("Number of data: " + numberOfData);

            _currentByte += 1;

            for (int i = 0; i < getNumberOfData(); i++)
            {
                var gpsElement = new GpsElement();

                ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 8)));

                var timeStamp = rb.ReadInt64();

                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);

                long cdrTimestamp = timeStamp;

                DateTime result = epochStart.AddMilliseconds(cdrTimestamp);

                Console.WriteLine("Time: " + result);

            }

        }


        public static int getNumberOfData()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2), 0);
            return numberOfData;
        }

    }

}
