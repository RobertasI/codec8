﻿using System;
using System.Linq;

namespace Codec8
{
    public static class StringConverter
    {
 

        public static byte[] StringToByteArray()
        {
            //string hex = "010000015908178fd8000f0fcaf1209a953c006900dd0e0022000e050101f0015001150351220142375308c700000036f10000601a520000000053000069de54000000205500000387570d4a9e50640000006f0001";
            string hex = "0400000113fc208dff000f14f650209cca80006f00d60400040004030101150316030001460000015d0000000113fc17610b000f14ffe0209cc580006e00c00500010004030101150316010001460000015e0000000113fc284945000f150f00209cd200009501080400000004030101150016030001460000015d0000000113fc267c5b000f150a50209cccc0009300680400000004030101150016030001460000015b0004";
            //string hex = "08180000000100620049095510B0513C154F561F276222621E359072633243101100000004B";
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static byte[] ReadBytes(this byte[] bytesArray, int skip, int take)
        {
            return bytesArray.Skip(skip).Take(take).ToArray();
        }
    }
}
