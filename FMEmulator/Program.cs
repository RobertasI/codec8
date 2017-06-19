using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("emulating started...");
            FMDataSimulator fm = new FMDataSimulator();
            var list = fm.GenerateAVLData();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
