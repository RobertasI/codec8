using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Domain
{
    [Table ("ServerLog")]
    public class ServerLog
    {
        public long Imei { get; set; }
        public DateTime Date { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}
