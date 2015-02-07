using System.Collections.Generic;
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
                db.SaveAllReferences(request);
                return Get(request);
            }
        }

        public PlayerResponse Put(Player request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var id = db.Update(request);
                return Get(new Player { Id = id });
            }
        }

        public PlayerResponse Delete(Player request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var id = db.Delete(request);
                return new PlayerResponse {Results = new List<Player> {request}};
            }
        }
    }
}
