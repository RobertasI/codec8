﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codec8Decoder;


namespace Checker
{
    class Program
    {
        static void Main(string[] args)
        {
            string hex = "0x080400000113FC208DFF000F14F650209CCA80006F00D60400040004030101150316030001460000015D0000000113FC17610B000F14FFE0209CC580006E00C00500010004030101150316010001460000015E0000000113FC284945000F150F00209CD200009501080400000004030101150016030001460000015D0000000113FC267C5B000F150A50209CCCC0009300680400000004030101150016030001460000015B0004";
            DataDecoder datadecoder = new Codec8Decoder.DataDecoder();
            var x = datadecoder.StringToByteArray(hex);

            Console.WriteLine(x);
        }
    }
}
