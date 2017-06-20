using System;
using System.Collections;
using Codec8;
namespace FMEmulator
{
    public class FMDataSimulator
    {
        private int numberOfData;
        private string timeStamp;
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
        ArrayList data = new ArrayList();


        public ArrayList GenerateAVLData()
        {

            
            Random random = new Random();
            numberOfData = random.Next(1, 6);
            var sNumberOfData = numberOfData.ToString("X1");
            data.Add(sNumberOfData);
            for (int i = 0; i < numberOfData; i++)
            {
                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
                int daysToAdd = random.Next(1000, 2000);
                var epochEnd = epochStart.AddDays(daysToAdd);
                timeStamp = ((int)((epochEnd - epochStart).TotalMilliseconds)).ToString("X8");
                priority = random.Next(0,2).ToString("X1");

                longitude = random.Next(0, 181).ToString("X4");
                latitude = random.Next(0, 91).ToString("X4");
                altitude = random.Next(0, 150).ToString("X2");
                angle = random.Next(1, 100).ToString("X2");
                sattelites = random.Next(1, 15).ToString("X1");
                speed = random.Next(0, 150).ToString("X2");

                ioEvent = random.Next(0, 2).ToString("X1");
                numberOfIOElementsInRecord = random.Next(1, 11);
                var stringNumberOfIOElementsInRecord = numberOfIOElementsInRecord.ToString("X1");

                #region Adding to list
                data.Add(timeStamp);
                data.Add(priority);
                data.Add(longitude);
                data.Add(latitude);
                data.Add(altitude);
                data.Add(angle);
                data.Add(sattelites);
                data.Add(speed);
                data.Add(ioEvent);
                data.Add(stringNumberOfIOElementsInRecord);
                #endregion

                NumberOfOneByteElements = random.Next(0, numberOfIOElementsInRecord);
                var sNumberOfOneByteElements = NumberOfOneByteElements.ToString("X1");
                data.Add(sNumberOfOneByteElements);
                for (int j = 0; j < numberOfIOElementsInRecord; j++)
                {
                    oneByteId = random.Next(1, 100).ToString("X1");
                    data.Add(oneByteId);
                    oneByteValue = random.Next(1,100).ToString("X1");
                    data.Add(oneByteValue);
                }

                numberOfTwobyteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements);
                var sNumberOfTwobyteElements = numberOfTwobyteElements.ToString("X1");
                data.Add(sNumberOfTwobyteElements);
                for (int j = 0; j < numberOfTwobyteElements; j++)
                {
                    twoBytesId = random.Next(1, 100).ToString("X1");
                    data.Add(twoBytesId);
                    twoBytesValue = random.Next(1, 100).ToString("X2");
                    data.Add(twoBytesValue);
                }

                numberOfFourByteElements = random.Next(0, numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements);
                var sNumberOfFourByteElements = numberOfFourByteElements.ToString("x1");
                data.Add(sNumberOfFourByteElements);
                for (int j = 0; j < numberOfFourByteElements; j++)
                {
                    fourBytesId = random.Next(1, 100).ToString("X1");
                    data.Add(fourBytesId);
                    fourBytesValue = random.Next(1, 100).ToString("X4");
                    data.Add(fourBytesValue);
                }

                numberOfEightbyteElements = numberOfIOElementsInRecord - NumberOfOneByteElements - numberOfTwobyteElements - numberOfFourByteElements;
                var sNumberOfEightbyteElements = numberOfEightbyteElements.ToString("X1");
                data.Add(sNumberOfEightbyteElements);
                for (int j = 0; j < numberOfEightbyteElements; j++)
                {
                    eightBytesId = random.Next(1, 100).ToString("X1");
                    data.Add(eightBytesId);
                    eightBytesValue = random.Next(1, 100).ToString("X8");
                    data.Add(eightBytesValue);
                }
            }

            DataEncoder dataencoder = new DataEncoder();
            DataDecoder dd = new DataDecoder();

            var byteArray = dataencoder.Encode(data);
            var listt = dd.Decode(byteArray);
            //foreach (var item in listt)
            //{
            //    Console.WriteLine(item);
            //}
            return listt;
        }
    }
}
