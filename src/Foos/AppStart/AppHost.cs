using Foos.Api.Operations;
using Foos.Api.Services;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.AppStart
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("HttpListener Self-Host", typeof(HelloService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            container.Register<IDbConnectionFactory>( //":memory:" for non-persistance, @"Data Source=foos.db;Version=3" for persistence
                new OrmLiteConnectionFactory(@"Data Source=foos.db;Version=3", SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().OpenDbConnection())
            {
                db.DropAndCreateTable<Match>();
                db.DropAndCreateTable<Team>();
                db.DropAndCreateTable<Player>();
                //db.CreateTableIfNotExists<Match>();
                //db.CreateTableIfNotExists<Team>();
                //db.CreateTableIfNotExists<Player>();
            }
        }
    }
}
