using System;
using Codec8;


namespace FMEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("emulating started...");
            FMDataSimulator fm = new FMDataSimulator();

            //var list = fm.GenerateAVLData();
            var list = StringConverter.StringToByteArray();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
