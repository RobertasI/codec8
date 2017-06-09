using Server.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Server.DataAccess
{
    class ServerLogInitializer : CreateDatabaseIfNotExists<ServerLogContext> //DropCreateDatabaseAlways<ServerLogContext>
    {

        public override void InitializeDatabase(ServerLogContext context)
        {
            //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
            //    string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(ServerLogContext context)
        {
            var serverLog = new List<ServerLog>
            {
                new ServerLog() { Imei = 123456789012345, Date = DateTime.Now, Latitude = 547146368, Longitude = 253032016 }
            };
            serverLog.ForEach(item => context.ServerLog.AddOrUpdate(item));
            context.SaveChanges();
        }
    }
}

