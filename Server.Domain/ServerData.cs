using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
