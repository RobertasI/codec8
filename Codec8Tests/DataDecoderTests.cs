using Codec8;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codec8Tests
{
    [TestFixture]
    public class DataDecoderTests
    {
        [Test]
        public void TestDecode()
        {
            DataDecoder dd = new DataDecoder();

            string hex = "010000015908178fd8000f0fcaf1209a953c006900dd0e0022000e050101f0015001150351220142375308c700000036f10000601a520000000053000069de54000000205500000387570d4a9e50640000006f0001";

            var bytesArray = Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();


            ArrayList expected = new ArrayList() { };
            //Assert.AreEqual(actual, expected);
        }

    }
}
