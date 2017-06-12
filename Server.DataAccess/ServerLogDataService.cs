using Server.Domain;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;


namespace Server.DataAccess
{
    public class ServerLogDataService
    {
        public List<ServerLog> GetAll()
        {
            using (var context = new ServerLogContext())
            {
                return context.ServerLog.ToList();
            }
        }

        public void Add(ServerLog serverlog)
        {
            using (var context = new ServerLogContext())
            {
                context.ServerLog.AddOrUpdate(serverlog);
                context.SaveChanges();
            }
        }
    }
}
  