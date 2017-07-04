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
                if (context.ServerLog.Any((o => (o.Imei == serverlog.Imei) && (o.Latitude == serverlog.Latitude) && (o.Longitude == serverlog.Longitude) && (o.Imei == 0)))) return; //cheking if data exist. if yes, return
                context.ServerLog.AddOrUpdate(serverlog);
                context.SaveChanges();
            }
        }
    }
}
  