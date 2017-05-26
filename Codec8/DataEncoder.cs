using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codec8
{
    class DataEncoder
    {
        public string[] Encode(ArrayList datalist)
        {
            
            String[] DataArray = (String[])datalist.ToArray(typeof(string));
            Console.WriteLine(DataArray);
            return DataArray;
        }
    }
}
