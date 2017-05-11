using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codec8Decoder
{
    class GpsElement
    {

        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public int Altitude { get; set; }
        public int Angle { get; set; }
        public int Satellites { get; set; }
        public int Speed { get; set; }

    }
}
