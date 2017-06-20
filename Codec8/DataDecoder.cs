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
            
            int numberOfData = BitConverter.ToInt16(StringConverter.ReadBytes(byteArray, 1, 2), 0);
            data.DataList.Add(numberOfData);
            
            for (int i = 0; i < numberOfData; i++)
            {
                using (ReversedBinaryReader rb = new ReversedBinaryReader(new MemoryStream(StringConverter.ReadBytes(byteArray, 2, byteArray.Length))))
                {
                    var timeStamp = rb.ReadInt64();

                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                    long cdrTimestamp = timeStamp;
                    DateTime result = epochStart.AddMilliseconds(cdrTimestamp);
                    data.DataList.Add(result);

                    int priority = rb.ReadByte();
                    data.DataList.Add(priority);

                    #region GPSElements
                    
                    gpsElement.Longitude = rb.ReadInt32();
                    data.DataList.Add(gpsElement.Longitude);

                    gpsElement.Latitude = rb.ReadInt32();
                    data.DataList.Add(gpsElement.Latitude);

                    gpsElement.Altitude = rb.ReadInt16();
                    data.DataList.Add(gpsElement.Altitude);

                    gpsElement.Angle = rb.ReadInt16();
                    data.DataList.Add(gpsElement.Angle);

                    gpsElement.Satellites = rb.ReadByte();
                    data.DataList.Add(gpsElement.Satellites);

                    gpsElement.Speed = rb.ReadInt16();
                    data.DataList.Add(gpsElement.Speed);
                    #endregion

                    #region IOelements
                    iotElement.EventID = rb.ReadByte();
                    data.DataList.Add(iotElement.EventID);

                    iotElement.NumberOfElements = rb.ReadByte();
                    data.DataList.Add(iotElement.NumberOfElements);

                    iotElement.numberOfOneByteElements = rb.ReadByte();
                    data.DataList.Add(iotElement.numberOfOneByteElements);

                    for (int j = 0; j < iotElement.numberOfOneByteElements; j++)
                    {
                        iotElement.oneByteID = rb.ReadByte();
                        data.DataList.Add(iotElement.oneByteID);

                        iotElement.oneByteValue = rb.ReadByte();
                        data.DataList.Add(iotElement.oneByteValue);
                    }

                    iotElement.numberOfTwoByteElements = rb.ReadByte();
                    data.DataList.Add(iotElement.numberOfTwoByteElements);

                    for (int j = 0; j < iotElement.numberOfTwoByteElements; j++)
                    {
                        iotElement.twoBytesID = rb.ReadByte();
                        data.DataList.Add(iotElement.twoBytesID);

                        iotElement.twoBytesValue = rb.ReadInt16();
                        data.DataList.Add(iotElement.twoBytesValue);
                    }

                    iotElement.numberOfFourByteElements = rb.ReadByte();
                    data.DataList.Add(iotElement.numberOfFourByteElements);

                    for (int j = 0; j < iotElement.numberOfFourByteElements; j++)
                    {
                        iotElement.fourBytesID = rb.ReadByte();
                        data.DataList.Add(iotElement.fourBytesID);

                        iotElement.fourBytesValue = rb.ReadInt32();
                        data.DataList.Add(iotElement.fourBytesValue);
                    }

                    iotElement.numberOfEightByteElements = rb.ReadByte();
                    data.DataList.Add(iotElement.numberOfEightByteElements);

                    for (int j = 0; j < iotElement.numberOfEightByteElements; j++)
                    {
                        iotElement.eightBytesID = rb.ReadByte();
                        data.DataList.Add(iotElement.eightBytesID);

                        iotElement.eightBytesValue = rb.ReadInt32();
                        data.DataList.Add(iotElement.eightBytesValue);
                    }
                    #endregion
                }
            }
            return data.DataList;
        }
    }
}


