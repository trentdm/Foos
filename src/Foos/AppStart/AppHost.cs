using System.Collections.Generic;
using System.IO;
using Foos.Api.Operations;
using Foos.Api.Services;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Validation;

namespace Foos.AppStart
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost() : base("HttpListener Self-Host", typeof(MatchService).Assembly) { }

        public override void Configure(Container container)
        {
            EnableLogging();
            EnableValidation(container);
            SetJsonCamelCase();
            EnableAutomaticContentReload();
            EnablePersistence(container);
            EnableAuthentication(container);
        }

        private void EnableValidation(Container container)
        {
            container.RegisterValidators(typeof(MatchService).Assembly);
            Plugins.Add(new ValidationFeature());
        }

        private static void SetJsonCamelCase()
        {
            JsConfig.EmitCamelCaseNames = true;
        }

        private void EnableLogging()
        {
            LogManager.LogFactory = new ConsoleLogFactory(debugEnabled: true);
            LogManager.LogFactory = new DebugLogFactory(debugEnabled: true);
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
//                db.DropTable<Match>();
//                db.DropTable<Team>();
//                db.DropTable<TeamMatch>();
//                db.DropTable<Player>();
//                db.DropTable<PlayerMatch>();
//                db.DropTable<UserPreferences>();
//#endif
                db.CreateTableIfNotExists<Match>();
                db.CreateTableIfNotExists<Team>();
                db.CreateTableIfNotExists<TeamMatch>();
                db.CreateTableIfNotExists<Player>();
                db.CreateTableIfNotExists<PlayerMatch>();
                db.CreateTableIfNotExists<Preferences>();

                if (!db.TableExists<Position>())
                {
                    db.CreateTable<Position>();
                    db.InsertAll(new List<Position>
                    {
                        new Position {Id = 0, Name = "Front"}, 
                        new Position {Id = 1, Name = "Back"}, 
                        new Position {Id = 2, Name = "Single"}
                    });
                }
            }
        }

        private void EnableAuthentication(Container container)
        {
            Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[] {
                new BasicAuthProvider(), //Sign-in with Basic Auth
                new CredentialsAuthProvider() //HTML Form post of UserName/Password credentials
            }) {IncludeAssignRoleServices = false}); //Not utilizing roles at this time, so simplifying API.

            Plugins.Add(new RegistrationFeature());

            container.Register<ICacheClient>(new MemoryCacheClient());
            var userRep = new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>());
            container.Register<IUserAuthRepository>(userRep);
            container.Resolve<IUserAuthRepository>().InitSchema();
        }
    }
}
