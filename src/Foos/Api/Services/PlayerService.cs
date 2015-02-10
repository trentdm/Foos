using Foos.Api.Operations;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Api.Services
{
    public class PlayerService : Service
    {
        private IDbConnectionFactory DbConnectionFactory { get; set; }

        public PlayerService(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public PlayerResponse Get(Player request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var players = request.Id == 0 ? db.LoadSelect<Player>()
                    : db.LoadSelect<Player>(t => t.Id == request.Id);
                return new PlayerResponse { Total = players.Count, Results = players };
            }
        }

        public PlayerResponse Post(Player request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request);
                return Get(request);
            }
        }

        public PlayerMatchResponse Get(PlayerMatch request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var players = request.Id == 0 ? db.LoadSelect<PlayerMatch>()
                    : db.LoadSelect<PlayerMatch>(t => t.Id == request.Id);
                return new PlayerMatchResponse { Total = players.Count, Results = players };
            }
        }

        public PlayerMatchResponse Post(PlayerMatch request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request, true);
                return Get(request);
            }
        }
    }
}
