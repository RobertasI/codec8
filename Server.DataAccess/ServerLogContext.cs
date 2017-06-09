using Server.Domain;
using System.Data.Entity;


namespace Server.DataAccess
{
    public class ServerLogContext : DbContext
    {
        public ServerLogContext() : base()
        {
            Database.SetInitializer(new ServerLogInitializer());
        }

        public DbSet<ServerLog> ServerLog { get; set; }

    }
}
