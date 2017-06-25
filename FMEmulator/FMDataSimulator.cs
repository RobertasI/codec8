using System;
using System.Collections;
using Codec8;
using System.Collections.Generic;
using System.Linq;

namespace FMEmulator
{
    public class FMDataSimulator
    {
        //private int numberOfData;
        //private string timeStamp;
        //private string priority;
        //private string longitude;
        //private string latitude;
        //private string altitude;
        //private string angle;
        //private string sattelites;
        //private string speed;
        //private string ioEvent;
        //private int numberOfIOElementsInRecord;
        //private int NumberOfOneByteElements;
        //private string oneByteId;
        //private string oneByteValue;
        //private int numberOfTwobyteElements;
        //private string twoBytesId;
        //private string twoBytesValue;
        //private int numberOfFourByteElements;
        //private string fourBytesId;
        //private string fourBytesValue;
        //private int numberOfEightbyteElements;
        //private string eightBytesId;
        //private string eightBytesValue;
        //ArrayList data = new ArrayList();
        Random random = new Random();

        //public ArrayList GenerateAVLData()
        //{

            
            
            //numberOfData = random.Next(1, 6);
            //var sNumberOfData = numberOfData.ToString("X2");
            //data.Add(sNumberOfData);
            //for (int i = 0; i < numberOfData; i++)
            //{
            //    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
            //    int daysToAdd = random.Next(10000, 20000);
            //    var epochEnd = epochStart.AddDays(daysToAdd);
            //    timeStamp = ((int)((epochEnd - epochStart).TotalMilliseconds)).ToString("X16");
            //    priority = random.Next(0,2).ToString("X2");

            //    longitude = random.Next(0, 181).ToString("X8");
            //    latitude = random.Next(0, 91).ToString("X8");
            //    altitude = random.Next(0, 150).ToString("X4");
            //    angle = random.Next(1, 100).ToString("X4");
            //    sattelites = random.Next(1, 15).ToString("X2");
            //    speed = random.Next(0, 150).ToString("X4");

            //    ioEvent = random.Next(0, 2).ToString("X2");
            //    numberOfIOElementsInRecord = random.Next(1, 11);
            //    var stringNumberOfIOElementsInRecord = numberOfIOElementsInRecord.ToString("X2");

            //    #region Adding to list
            //    data.Add(timeStamp);
            //    data.Add(priority);
            //    data.Add(longitude);
            //    data.Add(latitude);
            //    data.Add(altitude);
            //    data.Add(angle);
            //    data.Add(sattelites);
            //    data.Add(speed);
            //    data.Add(ioEvent);
            //    data.Add(stringNumberOfIOElementsInRecord);
            //    #endregion

            //    NumberOfOneByteElements = random.Next(0, numberOfIOElementsInRecord);
            //    var sNumberOfOneByteElements = NumberOfOneByteElements.ToString("X2");
            //    data.Add(sNumberOfOneByteElements);
            //    for (int j = 0; j < numberOfIOElementsInRecord; j++)
            //    {
            //        oneByteId = random.Next(1, 100).ToString("X2");
            //        data.Add(oneByteId);
            //        oneByteValue = random.Next(1,100).ToString("X2");
            //        data.Add(oneByteValue);
            //    }

            //    numberOfTwobyteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements);
            //    var sNumberOfTwobyteElements = numberOfTwobyteElements.ToString("X2");
            //    data.Add(sNumberOfTwobyteElements);
            //    for (int j = 0; j < numberOfTwobyteElements; j++)
            //    {
            //        twoBytesId = random.Next(1, 100).ToString("X2");
            //        data.Add(twoBytesId);
            //        twoBytesValue = random.Next(1, 100).ToString("X4");
            //        data.Add(twoBytesValue);
            //    }

            //    numberOfFourByteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements);
            //    var sNumberOfFourByteElements = numberOfFourByteElements.ToString("x2");
            //    data.Add(sNumberOfFourByteElements);
            //    for (int j = 0; j < numberOfFourByteElements; j++)
            //    {
            //        fourBytesId = random.Next(1, 100).ToString("X2");
            //        data.Add(fourBytesId);
            //        fourBytesValue = random.Next(1, 100).ToString("X8");
            //        data.Add(fourBytesValue);
            //    }

            //    numberOfEightbyteElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements - numberOfFourByteElements;
            //    var sNumberOfEightbyteElements = numberOfEightbyteElements.ToString("X2");
            //    data.Add(sNumberOfEightbyteElements);
            //    for (int j = 0; j < numberOfEightbyteElements; j++)
            //    {
            //        eightBytesId = random.Next(1, 100).ToString("X2");
            //        data.Add(eightBytesId);
            //        eightBytesValue = random.Next(1, 100).ToString("X16");
            //        data.Add(eightBytesValue);
                //}
            //}


        //    Console.WriteLine("---------bytes-------");
        //    foreach (var item in data)
        //    {
        //        Console.WriteLine(item);
        //    }
        //    Console.WriteLine("---------bytes-------");


        //    DataEncoder dataencoder = new DataEncoder();
        //    DataDecoder dd = new DataDecoder();

        //    var byteArray = dataencoder.Encode(data);
        //    var listt = dd.Decode(byteArray);

        //    return listt;
        //}

        public byte[] GenerateAVLDataBytes()
        {

            List<byte> data = new List<byte>();
            int numberOfData = random.Next(1, 6);
            data.AddRange(BitConverter.GetBytes(numberOfData));

            for (int i = 0; i < numberOfData; i++)
            {
                var timeStamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                Console.WriteLine("time: " + timeStamp);
                data.AddRange(BitConverter.GetBytes(timeStamp));

                byte priority = Convert.ToByte(random.Next(1, 2));
                data.Add(priority);

                #region GPSElements
                var longitude = BitConverter.GetBytes(random.Next(25000000, 30000000));
                data.AddRange(longitude);
                var latitude = BitConverter.GetBytes(random.Next(50000000, 600000000));
                data.AddRange(latitude);
                var altitude = BitConverter.GetBytes(Convert.ToInt16(random.Next(0, 32767)));
                data.AddRange(altitude);
                var angle = BitConverter.GetBytes(Convert.ToInt16(random.Next(0, 32767)));
                data.AddRange(angle);
                byte sattelites = Convert.ToByte(random.Next(1, 15));
                data.Add(sattelites);
                var speed = BitConverter.GetBytes(Convert.ToInt16(random.Next(0, 32767)));
                data.AddRange(speed);

                #endregion

                #region IOElements
                int numberOfIOElementsInRecord = random.Next(1, 11);
                data.Add(Convert.ToByte(numberOfIOElementsInRecord));

                int NumberOfOneByteElements = random.Next(1, numberOfIOElementsInRecord);
                data.Add(Convert.ToByte(NumberOfOneByteElements));

                for (int j = 0; j < numberOfIOElementsInRecord; j++)
                {
                    int oneByteId = random.Next(1, 100);
                    data.Add(Convert.ToByte(oneByteId));
                    int oneByteValue = random.Next(1, 100);
                    data.Add(Convert.ToByte(oneByteValue));
                }

                int numberOfTwobyteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements);
                data.Add(Convert.ToByte(numberOfTwobyteElements));

                for (int j = 0; j < numberOfTwobyteElements; j++)
                {
                    int twoBytesId = random.Next(1, 100);
                    data.Add(Convert.ToByte(twoBytesId));
                    int twoBytesValue = random.Next(1, 100);
                    data.Add(Convert.ToByte(twoBytesValue));
                }

                int numberOfFourByteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements);
                data.Add(Convert.ToByte(numberOfFourByteElements));
                for (int j = 0; j < numberOfFourByteElements; j++)
                {
                    int fourBytesId = random.Next(1, 100);
                    data.Add(Convert.ToByte(fourBytesId));
                    int fourBytesValue = random.Next(1, 100);
                    data.Add(Convert.ToByte(fourBytesValue));
                }

                int numberOfEightbyteElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements - numberOfFourByteElements;
                data.Add(Convert.ToByte(numberOfEightbyteElements));
                for (int j = 0; j < numberOfEightbyteElements; j++)
                {
                    int eightBytesId = random.Next(1, 100);
                    data.Add(Convert.ToByte(eightBytesId));
                    int eightBytesValue = random.Next(1, 100);
                    data.Add(Convert.ToByte(eightBytesValue));
                }
                #endregion

            }
            //foreach (var item in data)
            //{
            //    byte[] dataArray = item.ToArray();
            //}

            byte[] dataArray = data.ToArray();

            foreach (var item in dataArray)
            {
                Console.WriteLine(item);
            }
            return dataArray;
        }


        public double GenerateRandomDateTimeInMiliseconds()
        {

            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
            int daysToAdd = random.Next(1000, 2000);
            var randomDate = epochStart.AddDays(daysToAdd);
            var randomDateInMiliseconds = ((randomDate - epochStart).TotalMilliseconds);
            return randomDateInMiliseconds;
        }
    }
}
