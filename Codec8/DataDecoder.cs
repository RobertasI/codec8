using System;
using System.Collections;
using System.IO;

namespace Codec8
{
    public class DataDecoder
    {
        public static void Main()
        {
        }
         
        public ArrayList Decode(byte[] byteArray)
        {
            GpsElement gpsElement = new GpsElement();
            IotElement iotElement = new IotElement();
            Data data = new Data();

            using (ReversedBinaryReader reversedbinary = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(byteArray, 0, byteArray.Length))))
            {
                int numberOfData = reversedbinary.ReadByte();
                data.DataList.Add(numberOfData);

                for (int i = 0; i < numberOfData; i++)
            {

                    var timeStamp = reversedbinary.ReadInt64();

                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    DateTime time = epochStart.AddMilliseconds(timeStamp);
                    data.DataList.Add(time);

                    int priority = reversedbinary.ReadByte();
                    data.DataList.Add(priority);

                    #region GPSElements

                    gpsElement.Longitude = reversedbinary.ReadInt32();
                    data.DataList.Add(gpsElement.Longitude);

                    gpsElement.Latitude = reversedbinary.ReadInt32();
                    data.DataList.Add(gpsElement.Latitude);

                    gpsElement.Altitude = reversedbinary.ReadInt16();
                    data.DataList.Add(gpsElement.Altitude);

                    gpsElement.Angle = reversedbinary.ReadInt16();
                    data.DataList.Add(gpsElement.Angle);

                    gpsElement.Satellites = reversedbinary.ReadByte();
                    data.DataList.Add(gpsElement.Satellites);

                    gpsElement.Speed = reversedbinary.ReadInt16();
                    data.DataList.Add(gpsElement.Speed);
                    #endregion

                    #region IOelements
                    iotElement.EventID = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.EventID);

                    iotElement.NumberOfElements = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.NumberOfElements);

                    iotElement.numberOfOneByteElements = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.numberOfOneByteElements);


                    for (int j = 0; j < iotElement.numberOfOneByteElements; j++)
                    {
                        iotElement.oneByteID = reversedbinary.ReadByte();
                        data.DataList.Add(iotElement.oneByteID);

                        iotElement.oneByteValue = reversedbinary.ReadByte();
                        data.DataList.Add(iotElement.oneByteValue);
                    }

                    iotElement.numberOfTwoByteElements = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.numberOfTwoByteElements);
                    for (int m = 0; m < iotElement.numberOfTwoByteElements; m++)
                    {
                        iotElement.twoBytesID = reversedbinary.ReadByte();
                        data.DataList.Add(iotElement.twoBytesID);

                        iotElement.twoBytesValue = reversedbinary.ReadInt16();
                        data.DataList.Add(iotElement.twoBytesValue);
                    }

                    iotElement.numberOfFourByteElements = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.numberOfFourByteElements);

                    for (int n = 0; n < iotElement.numberOfFourByteElements; n++)
                    {
                        iotElement.fourBytesID = reversedbinary.ReadByte();
                        data.DataList.Add(iotElement.fourBytesID);

                        iotElement.fourBytesValue = reversedbinary.ReadInt32();
                        data.DataList.Add(iotElement.fourBytesValue);
                    }

                    iotElement.numberOfEightByteElements = reversedbinary.ReadByte();
                    data.DataList.Add(iotElement.numberOfEightByteElements);

                    for (int o = 0; o < iotElement.numberOfEightByteElements; o++)
                    {
                        iotElement.eightBytesID = reversedbinary.ReadByte();
                        data.DataList.Add(iotElement.eightBytesID);

                        iotElement.eightBytesValue = reversedbinary.ReadInt64();
                        data.DataList.Add(iotElement.eightBytesValue);
                    }
                    #endregion
                }
            }
            return data.DataList;
        }
    }
}


