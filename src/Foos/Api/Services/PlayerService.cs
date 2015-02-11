using System;
using System.Linq;
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

                foreach (var player in players)
                {
                    var playerMatches = db.LoadSelect<PlayerMatch>(pm => pm.PlayerId == player.Id);
                    var teamMatches = playerMatches.SelectMany(pm => db.LoadSelect<TeamMatch>(tm => pm.TeamMatchId == tm.Id)).ToList();
                    player.Wins = teamMatches.Count(tm => tm.IsWinner);
                    player.Losses = teamMatches.Count(tm => !tm.IsWinner);
                    player.WinAvg = Math.Round((double) player.Wins / teamMatches.Count, 3);
                    player.Points = playerMatches.Sum(pm => pm.Points);
                    player.PointsAvg = Math.Round((double) player.Points / teamMatches.Count, 3);
                }

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
