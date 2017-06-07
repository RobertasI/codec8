using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Server.DataAccess
{
    public class ServerLogDataService : IDisposable
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
                context.ServerLog.Add(serverlog);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
  