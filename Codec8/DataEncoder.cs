﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Codec8
{
    public class DataEncoder
    {
        public byte[] Encode(List<string> datalist)
        {
            var encodedToString = new StringBuilder();

            foreach (var item in datalist)
            {
                encodedToString.Append(item.ToString());
            }

            var stringData = encodedToString.ToString();
           
            return Enumerable.Range(0, stringData.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Byte.Parse(stringData.Substring(x, 2), NumberStyles.HexNumber))
                .ToArray();
        }
    }
}
