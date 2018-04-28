using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace YourTurnMyTurn
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private string sqliteConnectionString = "Data Source=yourturnmyturn.db;Version=3;";

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            // todo: db backend wrapper
            SetupDb();

            container.Register<IDbConnectionFactory>(
                (cContainer, overloads) =>
                    new OrmLiteConnectionFactory("ytmt.db", SqliteDialect.Provider));

            base.ConfigureApplicationContainer(container);
        }

        public void SetupDb()
        {
            //var factory = new OrmLiteConnectionFactory("todo connection string", SqliteDialect.Provider);
            //var db = factory.CreateDbConnection();
            //db.Open();

        }
        
    }
}
