using System;
using System.Collections;
using Codec8;
namespace FMEmulator
{
    public class FMDataSimulator
    {
        private int numberOfData;
        private DateTime timeStamp;
        private int priority;
        private int longitude;
        private int latitude;
        private int altitude;
        private int angle;
        private int sattelites;
        private int speed;
        private int ioEvent;
        private int numberOfIOElementsInRecord;
        private int NumberOfOneByteElements;
        private int oneByteId;
        private int oneByteValue;
        private int numberOfTwobyteElements;
        private int twoBytesId;
        private int twoBytesValue;
        private int numberOfFourByteElements;
        private int fourBytesId;
        private int fourBytesValue;
        private int numberOfEightbyteElements;
        private int eightBytesId;
        private long eightBytesValue;
        ArrayList data = new ArrayList();


        public byte[] GenerateAVLData()
        {

            
            Random random = new Random();
            numberOfData = random.Next(1, 6);
            data.Add(numberOfData);
            for (int i = 0; i < numberOfData; i++)
            {
                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                int daysToAdd = random.Next(10000, 20000);
                timeStamp = epochStart.AddDays(daysToAdd);
                priority = random.Next(0,2);

                longitude = random.Next(-180, 181);
                latitude = random.Next(-90, 91);
                altitude = random.Next(-10, 150);
                sattelites = random.Next(1, 15);
                angle = random.Next(1, 100);
                speed = random.Next(0, 150);

                ioEvent = random.Next(0, 2);
                numberOfIOElementsInRecord = random.Next(1, 11);

                #region Adding to list
                data.Add(timeStamp);
                data.Add(priority);
                data.Add(longitude);
                data.Add(latitude);
                data.Add(altitude);
                data.Add(sattelites);
                data.Add(angle);
                data.Add(speed);
                data.Add(ioEvent);
                data.Add(numberOfIOElementsInRecord);
                #endregion

                NumberOfOneByteElements = random.Next(0, numberOfIOElementsInRecord);
                data.Add(NumberOfOneByteElements);
                for (int j = 0; j < numberOfIOElementsInRecord; j++)
                {
                    oneByteId = random.Next(1, 100);
                    data.Add(oneByteId);
                    oneByteValue = random.Next(1,100);
                    data.Add(oneByteValue);

                }

                numberOfTwobyteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements);
                data.Add(numberOfTwobyteElements);
                for (int j = 0; j < numberOfTwobyteElements; j++)
                {
                    twoBytesId = random.Next(1, 100);
                    data.Add(twoBytesId);
                    twoBytesValue = random.Next(1, 100);
                    data.Add(twoBytesValue);
                }

                numberOfFourByteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements);
                data.Add(numberOfFourByteElements);
                for (int j = 0; j < numberOfFourByteElements; j++)
                {
                    fourBytesId = random.Next(1, 100);
                    data.Add(fourBytesId);
                    fourBytesValue = random.Next(1, 100);
                    data.Add(fourBytesValue);
                }

                numberOfEightbyteElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements - numberOfFourByteElements;
                data.Add(numberOfEightbyteElements);
                for (int j = 0; j < numberOfEightbyteElements; j++)
                {
                    eightBytesId = random.Next(1, 100);
                    data.Add(eightBytesId);
                    eightBytesValue = random.Next(1, 100);
                    data.Add(eightBytesValue);
                }
            }

            DataEncoder dataencoder = new DataEncoder();
            var byteArray = dataencoder.Encode(data);
            return byteArray;            
        }
    }
}
