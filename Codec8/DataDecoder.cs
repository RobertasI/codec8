﻿using Codec8;
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

            int _currentByte = 1;


            int numberOfData = getNumberOfData();
            Console.WriteLine("Number of data: " + numberOfData);

            _currentByte += 1;

            for (int i = 0; i < getNumberOfData(); i++)
            {
               // var gpsElement = new GpsElement();

                ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 8)));

                var timeStamp = rb.ReadInt64();

                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                long cdrTimestamp = timeStamp;
                DateTime result = epochStart.AddMilliseconds(cdrTimestamp);
                Console.WriteLine("Time: " + result);

                _currentByte += 8;

                //skippig priority
                _currentByte += 1;

                
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

                //var eventId = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2), 0);
                //Console.WriteLine("IO element ID of  Event generated: " + eventId);
                _currentByte += 1;


                //skipping all IO records
                _currentByte += 1;


                int oneByteElements = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2), 0);
                Console.WriteLine("one byte elements: " + oneByteElements);

                ReversedBinaryReader rb6 = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, 2)));
                int oneByteElements2 = rb6.ReadInt16();
                Console.WriteLine("one byte elements2: " + oneByteElements2);
                _currentByte += 2;

                //for (int j = 0; j)
            }

        }


        public static int getNumberOfData()
        {
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(StringConverter.StringToByteArray(), 1, 2), 0);
            return numberOfData;
        }

    }

}
