using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Server.Domain;

namespace Server.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<ServerData> ServerData { get; set; }
    }
}
