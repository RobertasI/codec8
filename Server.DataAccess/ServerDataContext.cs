using System.Data.Entity;
using Server.Domain;

namespace Server.DataAccess
{
    public class ServerDataContext : DbContext
    {
        public DbSet<ServerData> ServerData { get; set; }
    }
}
