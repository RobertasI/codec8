using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Domain
{
    [Table("ServerData")]
    public class ServerData
    {
        public long Imei { get; set; }
        public int AvlHeader { get; set; }
        public ArrayList AvlData { get; set; }
    }
}
