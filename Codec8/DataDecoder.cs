using System;
using System.Collections;
using System.IO;

namespace Codec8
{
    public class DataDecoder
    {
        //public static void Main()
        //{
        //    byte[] byteArray = StringConverter.StringToByteArray();
        //    Decode(byteArray);

        //    DataEncoder dataencoder = new DataEncoder();

        //    byte[] encoded =  dataencoder.Encode(Decode(byteArray));
        //    Console.WriteLine(encoded);
        //}
        
        public static ArrayList Decode(byte[] byteArray)
        {
            GpsElement gpsElement = new GpsElement();
            IotElement iotElement = new IotElement();
            Data data = new Data();


            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(byteArray, 1, 2), 0);
            data.DataList.Add(numberOfData);
            Console.WriteLine("Number of data: " + numberOfData);


            for (int i = 0; i < numberOfData; i++)
            {
                using (ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(byteArray, 2, StringConverter.StringToByteArray().Length))))
                {

                    var timeStamp = rb.ReadInt64();

                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                    long cdrTimestamp = timeStamp;
                    DateTime result = epochStart.AddMilliseconds(cdrTimestamp);
                    Console.WriteLine("Time: " + result);
                    data.DataList.Add(timeStamp);

                    int priority = rb.ReadByte();
                    Console.WriteLine("Priority: " + priority);
                    data.DataList.Add(priority);

                    #region GPSElements

                    gpsElement.Longitude = rb.ReadInt32();
                    Console.WriteLine("Longitude: " + gpsElement.Longitude);
                    data.DataList.Add(gpsElement.Longitude);

                    gpsElement.Latitude = rb.ReadInt32();
                    Console.WriteLine("Latitude: " + gpsElement.Latitude);
                    data.DataList.Add(gpsElement.Latitude);

                    gpsElement.Altitude = rb.ReadInt16();
                    Console.WriteLine("Altitude: " + gpsElement.Altitude);
                    data.DataList.Add(gpsElement.Altitude);

                    gpsElement.Angle = rb.ReadInt16();
                    Console.WriteLine("Angle: " + gpsElement.Angle);
                    data.DataList.Add(gpsElement.Angle);

                    gpsElement.Satellites = rb.ReadByte();
                    Console.WriteLine("Satellites: " + gpsElement.Satellites);
                    data.DataList.Add(gpsElement.Satellites);

                    gpsElement.Speed = rb.ReadInt16();
                    Console.WriteLine("Speed: " + gpsElement.Speed);
                    data.DataList.Add(gpsElement.Speed);
                    #endregion


                    iotElement.EventID = rb.ReadByte();
                    Console.WriteLine("IO element ID of  Event generated: " + iotElement.EventID);
                    data.DataList.Add(iotElement.EventID);

                    iotElement.NumberOfElements = rb.ReadByte();
                    Console.WriteLine("IO elements in record: " + iotElement.NumberOfElements);
                    data.DataList.Add(iotElement.NumberOfElements);

                    iotElement.numberOfOneByteElements = rb.ReadByte();
                    Console.WriteLine("One byte elements in record: " + iotElement.numberOfOneByteElements);
                    data.DataList.Add(iotElement.numberOfOneByteElements);

                    for (int j = 0; j < iotElement.numberOfOneByteElements; j++)
                    {
                        iotElement.oneByteID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + iotElement.oneByteID);
                        data.DataList.Add(iotElement.oneByteID);

                        iotElement.oneByteValue = rb.ReadByte();
                        Console.WriteLine(iotElement.oneByteID + "th IO element's value = " + iotElement.oneByteValue);
                        data.DataList.Add(iotElement.oneByteValue);

                    }

                    iotElement.numberOfTwoByteElements = rb.ReadByte();
                    Console.WriteLine("Two bytes elements in record: " + iotElement.numberOfTwoByteElements);
                    data.DataList.Add(iotElement.numberOfTwoByteElements);

                    for (int j = 0; j < iotElement.numberOfTwoByteElements; j++)
                    {
                        iotElement.twoBytesID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + iotElement.twoBytesID);
                        data.DataList.Add(iotElement.twoBytesID);

                        iotElement.twoBytesValue = rb.ReadInt16();
                        Console.WriteLine(iotElement.twoBytesID + "th IO element's value = " + iotElement.twoBytesValue);
                        data.DataList.Add(iotElement.twoBytesValue);
                    }


                    iotElement.numberOfFourByteElements = rb.ReadByte();
                    Console.WriteLine("Four bytes elements in record: " + iotElement.numberOfFourByteElements);
                    data.DataList.Add(iotElement.numberOfFourByteElements);

                    for (int j = 0; j < iotElement.numberOfFourByteElements; j++)
                    {
                        iotElement.fourBytesID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + iotElement.fourBytesID);
                        data.DataList.Add(iotElement.fourBytesID);

                        iotElement.fourBytesValue = rb.ReadInt32();
                        Console.WriteLine(iotElement.fourBytesID + "th IO element's value = " + iotElement.fourBytesValue);
                        data.DataList.Add(iotElement.fourBytesValue);
                    }

                    iotElement.numberOfEightByteElements = rb.ReadByte();
                    Console.WriteLine("Eight bytes elements in record: " + iotElement.numberOfEightByteElements);
                    data.DataList.Add(iotElement.numberOfEightByteElements);

                    for (int j = 0; j < iotElement.numberOfEightByteElements; j++)
                    {
                        iotElement.eightBytesID = rb.ReadByte();
                        Console.WriteLine("IO element ID = " + iotElement.eightBytesID);
                        data.DataList.Add(iotElement.eightBytesID);

                        iotElement.eightBytesValue = rb.ReadInt32();
                        Console.WriteLine(iotElement.eightBytesID + "th IO element's value = " + iotElement.eightBytesValue);
                        data.DataList.Add(iotElement.eightBytesValue);
                    }


                    foreach(var item in data.DataList)
                    {
                        Console.WriteLine(item);
                    }
                }
            }

            return data.DataList;
        }

    }
}


