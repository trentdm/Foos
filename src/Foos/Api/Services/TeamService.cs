using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Foos.Api.Operations;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Api.Services
{
    public class TeamService : Service
    {
        private IDbConnectionFactory DbConnectionFactory { get; set; }

        public TeamService(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public TeamResponse Get(Team request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var teams = request.Id == 0 ? db.LoadSelect<Team>()
                    : db.LoadSelect<Team>(t => t.Id == request.Id);

                foreach (var team in teams)
                {
                    var teamMatches = db.LoadSelect<TeamMatch>(tm => tm.TeamId == team.Id);
                    team.Players = GetTopPlayerNames(db, teamMatches);
                    team.Wins = teamMatches.Count(tm => tm.IsWinner);
                    team.Losses = teamMatches.Count(tm => !tm.IsWinner);
                    team.WinAvg = Math.Round((double) team.Wins / teamMatches.Count, 3);
                    team.Points = teamMatches.Sum(pm => pm.Score);
                    team.PointsAvg = Math.Round((double) team.Points / teamMatches.Count, 3);
                }

                return new TeamResponse {Total = teams.Count, Results = teams};
            }
        }

        private string GetTopPlayerNames(IDbConnection db, IEnumerable<TeamMatch> teamMatches)
        {
            var playerMatches = teamMatches.SelectMany(tm => db.LoadSelect<PlayerMatch>(pm => pm.TeamMatchId == tm.Id)).ToList();
            var players = playerMatches.SelectMany(pm => db.LoadSelect<Player>(p => p.Id == pm.PlayerId))
                .GroupBy(p => p.Id).Select(g => g.First()).ToList();

            foreach (var player in players)
            {
                player.Points = playerMatches.Where(pm => pm.PlayerId == player.Id).Sum(pm => pm.Points);
            }

            var topPlayers = players.OrderByDescending(p => p.Points).Take(4);
            var playerString = string.Join(", ", topPlayers.Select(p => p.Name));
            if (players.Count > 4)
                playerString += "...";
            return playerString;
        }

        public TeamResponse Post(Team request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request);
                return Get(request);
            }
        }

        public TeamMatchResponse Get(TeamMatch request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var teams = request.Id == 0 ? db.LoadSelect<TeamMatch>()
                    : db.LoadSelect<TeamMatch>(t => t.Id == request.Id);
                return new TeamMatchResponse { Total = teams.Count, Results = teams };
            }
        }

        public TeamMatchResponse Post(TeamMatch request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request, true);
                return Get(request);
            }
        }
    }
}
