using System.IO;
using Foos.Api.Operations;
using Foos.Api.Services;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Foos.AppStart
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost() : base("HttpListener Self-Host", typeof(HelloService).Assembly) { }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
                {
                    HandlerFactoryPath = "api",
                });
            SetJsonCamelCase();
            EnableLogging();
            EnableAutomaticContentReload();
            EnablePersistence(container);
            EnableAuthentication(container);
        }

        private static void SetJsonCamelCase()
        {
            JsConfig.EmitCamelCaseNames = true;
        }

        private void EnableLogging()
        {
            Plugins.Add(new RequestLogsFeature());
        }

        private void EnableAutomaticContentReload()
        {
            //for automatic reload of running content after saving changes in IDE
            SetConfig(new HostConfig
                {
                #if DEBUG
                    DebugMode = true,
                    WebHostPhysicalPath = Path.GetFullPath(Path.Combine("~".MapServerPath(), "..", "..")),
                #endif
                });
        }

        private static void EnablePersistence(Container container)
        {
            container.Register<IDbConnectionFactory>( //":memory:" for non-persistance, @"Data Source=foos.db;Version=3" for persistence
                new OrmLiteConnectionFactory(@"Data Source=foos.db;Version=3", SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().OpenDbConnection())
            {
                //#if DEBUG
                //db.DropTable<Match>();
                //db.DropTable<Team>();
                //db.DropTable<Player>();
                //#endif
                db.CreateTableIfNotExists<Match>();
                db.CreateTableIfNotExists<Team>();
                db.CreateTableIfNotExists<Player>();
            }
        }

        private void EnableAuthentication(Container container)
        {
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                                        new IAuthProvider[]
                                            {
                                                new BasicAuthProvider(), //Sign-in with Basic Auth
                                                new CredentialsAuthProvider() //HTML Form post of UserName/Password credentials
                                            }));

            Plugins.Add(new RegistrationFeature());

            container.Register<ICacheClient>(new MemoryCacheClient());
            var userRep = new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>());
            container.Register<IUserAuthRepository>(userRep);
            container.Resolve<IUserAuthRepository>().InitSchema();
        }
    }
}
