using System;
using Codec8;
using System.Collections.Generic;

namespace FMEmulator
{
    public class FMDataSimulator
    {
        private int numberOfData;
        private string time = "";
        private string priority;
        private string longitude;
        private string latitude;
        private string altitude;
        private string angle;
        private string sattelites;
        private string speed;
        private string ioEvent;
        private int numberOfIOElementsInRecord;
        private int NumberOfOneByteElements;
        private string oneByteId;
        private string oneByteValue;
        private int numberOfTwobyteElements;
        private string twoBytesId;
        private string twoBytesValue;
        private int numberOfFourByteElements;
        private string fourBytesId;
        private string fourBytesValue;
        private int numberOfEightbyteElements;
        private string eightBytesId;
        private string eightBytesValue;
        Random random = new Random();

        public byte[] GenerateAVLData()
        {

            List<string> data = new List<string>();

            numberOfData = random.Next(10, 20);
            var sNumberOfData = numberOfData.ToString("X2");
            data.Add(sNumberOfData);
            for (int i = 0; i < numberOfData; i++)
            {

                time = ((long)((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds)).ToString("X16");
                priority = random.Next(0, 2).ToString("X2");
                data.Add(time);
                data.Add(priority);              

                #region GPSElements
                longitude = random.Next(23000000, 26000000).ToString("X8");
                latitude = random.Next(53000000, 56000000).ToString("X8");
                altitude = random.Next(0, 32767).ToString("X4");
                angle = random.Next(0, 32767).ToString("X4");
                sattelites = random.Next(1, 15).ToString("X2");
                speed = random.Next(0, 150).ToString("X4");

                data.Add(longitude);
                data.Add(latitude);
                data.Add(altitude);
                data.Add(angle);
                data.Add(sattelites);
                data.Add(speed);
                
                #endregion

                ioEvent = random.Next(2, 2).ToString("X2");
                data.Add(ioEvent);

                numberOfIOElementsInRecord = random.Next(1, 4);
                var stringNumberOfIOElementsInRecord = numberOfIOElementsInRecord.ToString("X2");
                data.Add(stringNumberOfIOElementsInRecord);

                NumberOfOneByteElements = random.Next(0, numberOfIOElementsInRecord);
                var sNumberOfOneByteElements = NumberOfOneByteElements.ToString("X2");
                data.Add(sNumberOfOneByteElements);

                for (int j = 0; j < NumberOfOneByteElements; j++)
                {
                    oneByteId = random.Next(1, 100).ToString("X2");
                    data.Add(oneByteId);
                    oneByteValue = random.Next(1, 100).ToString("X2");
                    data.Add(oneByteValue);
                }

                int maxNumberOfTwoBytesElements = numberOfIOElementsInRecord - NumberOfOneByteElements;

                numberOfTwobyteElements = random.Next(0, maxNumberOfTwoBytesElements);
                var sNumberOfTwobyteElements = numberOfTwobyteElements.ToString("X2");
                data.Add(sNumberOfTwobyteElements);
                for (int m = 0; m < numberOfTwobyteElements; m++)
                {
                    twoBytesId = random.Next(1, 100).ToString("X2");
                    data.Add(twoBytesId);
                    twoBytesValue = random.Next(1, 100).ToString("X4");
                    data.Add(twoBytesValue);
                }

                int maxNumberOfFourBytesElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements;

                numberOfFourByteElements = random.Next(0, maxNumberOfFourBytesElements);
                var sNumberOfFourByteElements = numberOfFourByteElements.ToString("x2");
                data.Add(sNumberOfFourByteElements);
                for (int n = 0; n < numberOfFourByteElements; n++)
                {
                    fourBytesId = random.Next(1, 100).ToString("X2");
                    data.Add(fourBytesId);
                    fourBytesValue = random.Next(1, 100).ToString("X8");
                    data.Add(fourBytesValue);
                }


                numberOfEightbyteElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements - numberOfFourByteElements;
                var sNumberOfEightbyteElements = numberOfEightbyteElements.ToString("X2");
                data.Add(sNumberOfEightbyteElements);
                for (int o = 0; o < numberOfEightbyteElements; o++)
                {
                    eightBytesId = random.Next(1, 100).ToString("X2");
                    data.Add(eightBytesId);
                    eightBytesValue = random.Next(1, 100).ToString("X16");
                    data.Add(eightBytesValue);
                }
            }
            data.Add(sNumberOfData);

            DataEncoder dataencoder = new DataEncoder();

            var byteArray = dataencoder.Encode(data);
            return byteArray;
        }      

        
    }
}
