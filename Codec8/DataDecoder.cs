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
                using (ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(StringConverter.StringToByteArray(), _currentByte, StringConverter.StringToByteArray().Length))))
                {

                    var timeStamp = rb.ReadInt64();

                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                    long cdrTimestamp = timeStamp;
                    DateTime result = epochStart.AddMilliseconds(cdrTimestamp);
                    Console.WriteLine("Time: " + result);

                    _currentByte += 8;

                    int priority = rb.ReadByte();
                    Console.WriteLine("Priority: " + priority);
                    _currentByte += 1;

                    #region GPSElements

                    gpsElement.Longitude = rb.ReadInt32();
                    Console.WriteLine("Longitude: " + gpsElement.Longitude);
                    _currentByte += 4;

                    gpsElement.Latitude = rb.ReadInt32();
                    Console.WriteLine("Latitude: " + gpsElement.Latitude);
                    _currentByte += 4;

                    gpsElement.Altitude = rb.ReadInt16();
                    Console.WriteLine("Altitude: " + gpsElement.Altitude);
                    _currentByte += 2;

                    gpsElement.Angle = rb.ReadInt16();
                    Console.WriteLine("Angle: " + gpsElement.Angle);
                    _currentByte += 2;

                    gpsElement.Satellites = rb.ReadByte();
                    Console.WriteLine("Satellites: " + gpsElement.Satellites);
                    _currentByte += 1;

                    gpsElement.Speed = rb.ReadInt16();
                    Console.WriteLine("Speed: " + gpsElement.Speed);
                    _currentByte += 2;
                    #endregion

                    iotElement.EventID = rb.ReadByte();
                    Console.WriteLine("IO element ID of  Event generated: " + iotElement.EventID);
                    _currentByte += 1;
                    

                    iotElement.NumberOfElements = rb.ReadByte();
                    Console.WriteLine("IO elements in record: " + iotElement.NumberOfElements);
                    _currentByte += 1;

                    int oneByteElements = rb.ReadByte();
                    Console.WriteLine("One byte elements in record: " + oneByteElements);
                    _currentByte += 1;


                    for (int j = 0; j < oneByteElements; j++)
                    {
                        byte oneByteID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + oneByteID);
                        _currentByte += 1;

                        byte oneByteValue = rb.ReadByte();
                        Console.WriteLine(oneByteID + "th IO element's value = " + oneByteValue);
                        _currentByte += 1;

                        iotElement.oneByte.Add(oneByteID, oneByteValue);
                    }

                    int twoBytesElements = rb.ReadByte();
                    Console.WriteLine("Two bytes elements in record: " + twoBytesElements);
                    _currentByte += 1;

                    for (int j = 0; j < twoBytesElements; j++)
                    {
                        byte twoByteID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + twoByteID);
                        _currentByte += 1;


                        int twoByteValue = rb.ReadInt16();
                        Console.WriteLine(twoByteID + "th IO element's value = " + twoByteValue);
                        _currentByte += 2;

                        iotElement.twoBytes.Add(twoByteID, twoByteValue);
                    }

                    int fourBytesElements = rb.ReadByte();
                    Console.WriteLine("Four bytes elements in record: " + fourBytesElements);
                    _currentByte += 1;




                    for (int j = 0; j < fourBytesElements; j++)
                    {
                        byte fourBytesID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + fourBytesID);
                        _currentByte += 1;

                        int fourBytesValue = rb.ReadInt32();
                        Console.WriteLine(fourBytesID + "th IO element's value = " + fourBytesValue);
                        _currentByte += 4;

                        iotElement.fourBytes.Add(fourBytesID, fourBytesValue);
                    }

                    int eightBytesElements = rb.ReadByte();
                    Console.WriteLine("Eight bytes elements in record: " + eightBytesElements);
                    _currentByte += 1;

                    for (int j = 0; j < eightBytesElements; j++)
                    {
                        byte eightBytesID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + eightBytesID);
                        _currentByte += 1;

                        int eightBytesValue = rb.ReadInt32();
                        Console.WriteLine(eightBytesID + "th IO element's value = " + eightBytesValue);
                        _currentByte += 8;

                        iotElement.eightBytes.Add(eightBytesID, eightBytesValue);
                    }
                    //susikurti klasę duomenų encodinimui
                    //pasidaryti, kad pridėtų į listą

                    data.DataList.Add(iotElement.NumberOfElements);
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


