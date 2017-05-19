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
            GpsElement gpsElement = new GpsElement();
            IotElement iotElement = new IotElement();
            Data data = new Data();

            int _currentByte = 1;


            int numberOfData = getNumberOfData();
            Console.WriteLine("Number of data: " + numberOfData);

            _currentByte += 1;

            for (int i = 0; i < getNumberOfData(); i++)
            {
                using(ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, StringConverter.StringToByteArray().Length))))
                {

                }



                var timeStamp = rb.ReadInt64();

                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                long cdrTimestamp = timeStamp;
                DateTime result = epochStart.AddMilliseconds(cdrTimestamp);
                Console.WriteLine("Time: " + result);

                _currentByte += 8;

                //skippig priority
                _currentByte += 1;

                #region GPSElements

                ReversedBinaryReader rb1 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 4)));
                gpsElement.Longitude = rb1.ReadInt32();
                Console.WriteLine("Longitude: " + gpsElement.Longitude);               
                _currentByte += 4;

                ReversedBinaryReader rb2 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 4)));
                gpsElement.Latitude = rb2.ReadInt32();
                Console.WriteLine("Latitude: " + gpsElement.Latitude);
                _currentByte += 4;

                ReversedBinaryReader rb3 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2)));
                gpsElement.Altitude = rb3.ReadInt16();
                Console.WriteLine("Altitude: " + gpsElement.Altitude);
                _currentByte += 2;

                ReversedBinaryReader rb4 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2)));
                gpsElement.Angle = rb4.ReadInt16();
                Console.WriteLine("Angle: " + gpsElement.Angle);
                _currentByte += 2;

                gpsElement.Satellites = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2), 0);
                Console.WriteLine("Satellites: " + gpsElement.Satellites);
                _currentByte += 1;

                ReversedBinaryReader rb5 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2)));
                gpsElement.Speed = rb5.ReadInt16();
                Console.WriteLine("Speed: " + gpsElement.Speed);
                _currentByte += 2;
                #endregion

                int ioElement = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                Console.WriteLine("IO element ID of  Event generated: " + ioElement);
                _currentByte += 1;


                int ioElements = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                Console.WriteLine("IO elements in record: " + ioElements);
                _currentByte += 1;

                int oneByteElements = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                Console.WriteLine("One byte elements in record: " + oneByteElements);
                _currentByte += 1;


                for (int j = 0; j < oneByteElements; j++)
                {
                    int oneByteID = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                    Console.WriteLine("IO element ID = " + oneByteID);
                    _currentByte += 1;

                    int oneByteValue = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                    Console.WriteLine(oneByteID + "th IO element's value = " + oneByteValue);
                    _currentByte += 1;
                }

                int twoBytesElements = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                Console.WriteLine("Two bytes elements in record: " + twoBytesElements);
                _currentByte += 1;

                for(int j = 0; j < twoBytesElements; j++)
                {
                    int twoByteID = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                    Console.WriteLine("IO element ID = " + twoByteID);
                    _currentByte += 1;

                    ReversedBinaryReader rb6 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2)));
                    int twoByteValue = rb6.ReadInt16();
                    Console.WriteLine(twoByteID + "th IO element's value = " + twoByteValue);
                    _currentByte += 2;

                }

                int fourBytesElements = (int)(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2))[0];
                Console.WriteLine("Four bytes elements in record: " + fourBytesElements);
                _currentByte += 1;


                using (ReversedBinaryReader rb7 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, (4*fourBytesElements + fourBytesElements)))))
                {

                    for (int j = 0; j < fourBytesElements; j++)
                    {
                        int fourByteID = rb7.ReadByte();
                        Console.WriteLine("IO element ID = " + fourByteID);
                        _currentByte += 1;

                        //ReversedBinaryReader rb7 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 4)));
                        int fourByteValue = rb7.ReadInt32();
                        Console.WriteLine(fourByteID + "th IO element's value = " + fourByteValue);
                        _currentByte += 4;

                    }
                }

              

            }

        }


        public static int getNumberOfData()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2), 0);
            return numberOfData;
        }

    }

}
