using System;
using System.Collections.Generic;
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
                var teamMatches = db.LoadSelect<TeamMatch>();

                foreach (var tm in teamMatches)
                {
                    tm.PlayerMatches = db.LoadSelect<PlayerMatch>(pm => pm.TeamMatchId == tm.Id);

                    foreach (var pm in tm.PlayerMatches)
                        pm.Player = db.LoadSingleById<Player>(pm.PlayerId);

                    tm.PlayerMatches = tm.PlayerMatches.Where(pm => pm.TeamMatchId == tm.Id).ToList();
                    var players = tm.PlayerMatches.Select(pm => pm.Player).OrderBy(p => p.Id).ToList();
                    tm.Team = new Team
                    {
                        Name = string.Join("/", players.Select(p => p.Name)),
                        Players = players
                    };
                }

                var teams = new List<Team>();
                var groups = teamMatches.GroupBy(tm => tm.Team.Name);

                foreach (var group in groups)
                {
                    var team = group.First().Team;
                    team.Wins = group.Count(tm => tm.IsWinner);
                    team.Losses = group.Count(tm => !tm.IsWinner);
                    team.Games = group.Count();
                    team.WinAvg = Math.Round((double)team.Wins / group.Count(), 3)*100;
                    team.Points = group.Sum(pm => pm.Score);
                    team.PointsAvg = Math.Round((double)team.Points / group.Count(), 3);
                    teams.Add(team);
                }

                var orderedTeams = teams.OrderByDescending(t => t.Wins > 1).ThenByDescending(t => t.WinAvg).ThenByDescending(t => t.Wins).ToList();
                return new TeamResponse {Total = teams.Count, Results = orderedTeams};
            }
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
