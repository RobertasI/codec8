using Server.Domain;
using System.Data.Entity;


namespace Server.DataAccess
{
    public class ServerLogContext : DbContext
    {
        public DbSet<ServerLog> ServerLog { get; set; }

    }
}
