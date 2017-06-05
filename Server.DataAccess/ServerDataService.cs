using Server.Domain;
using System.Collections.Generic;
using System.Linq;


namespace Server.DataAccess
{
    public class ServerDataService
    {
        public List<ServerData> GetAll()
        {
            using (var context = new ServerDataContext())
            {
                return context.ServerData.ToList();
            }
        }

        public void Add(ServerData serverData)
        {
            using  (var context = new ServerDataContext())
            {
                context.ServerData.Add(serverData);
                context.SaveChanges();
            }
        }
    }
}
