using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Foos.Api.Operations;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Api.Services
{
    public class MatchService : Service
    {
        private IDbConnectionFactory DbConnectionFactory { get; set; }
        private AuthUserSession UserSession
        {
            get
            {
                try { return base.SessionAs<AuthUserSession>(); }
                catch { return new AuthUserSession(); } //fallback for unittesting
            }
        }

        public MatchService(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public MatchResponse Get(Match request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var matches = request.Id == 0 ? 
                    db.LoadSelect<Match>()
                    : db.LoadSelect<Match>(t => t.Id == request.Id);

                foreach (var match in matches)
                {
                    if(!string.IsNullOrEmpty(UserSession.Id))
                        match.UserAuthName =  db.SingleById<UserAuth>(match.UserAuthId).UserName;

                    match.TeamMatches = db.LoadSelect<TeamMatch>(tm => tm.MatchId == match.Id);

                    foreach (var teamMatch in match.TeamMatches)
                    {
                        teamMatch.PlayerMatches = db.LoadSelect<PlayerMatch>(pm => pm.TeamMatchId == teamMatch.Id);

                        foreach (var playerMatch in teamMatch.PlayerMatches)
                        {
                            playerMatch.Player = db.SingleById<Player>(playerMatch.PlayerId);
                        }

                        teamMatch.Team = db.SingleById<Team>(teamMatch.TeamId);
                        teamMatch.Team.Name = string.Join("/", GetTeamNames(teamMatch));
                    }
                }

                return new MatchResponse { Total = matches.Count, Results = matches.OrderByDescending(m => m.DateTime).ToList() };
            }
        }

        public SearchPlayerGamesResponse Get(SearchPlayerGames request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var matches = db.LoadSelect<Match>();

                foreach (var match in matches)
                {
                    if (!string.IsNullOrEmpty(UserSession.Id))
                        match.UserAuthName = db.SingleById<UserAuth>(match.UserAuthId).UserName;

                    match.TeamMatches = db.LoadSelect<TeamMatch>(tm => tm.MatchId == match.Id);

                    foreach (var teamMatch in match.TeamMatches)
                    {
                        teamMatch.PlayerMatches = db.LoadSelect<PlayerMatch>(pm => pm.TeamMatchId == teamMatch.Id);

                        foreach (var playerMatch in teamMatch.PlayerMatches)
                        {
                            playerMatch.Player = db.SingleById<Player>(playerMatch.PlayerId);
                        }

                        teamMatch.Team = db.SingleById<Team>(teamMatch.TeamId);
                        teamMatch.Team.Name = string.Join("/", GetTeamNames(teamMatch));
                    }
                }

                var returnMatches =
                    matches.Where(
                        p => p.TeamMatches.Any(q => q.PlayerMatches.Any(r => r.Player.Name.Equals(request.PlayerName))));

                return new SearchPlayerGamesResponse { Total = matches.Count, Results = returnMatches.OrderByDescending(m => m.DateTime).ToList() };
            }
        }

        private IEnumerable<string> GetTeamNames(TeamMatch teamMatch)
        {
            return teamMatch.PlayerMatches.Select(pm => pm.Player).OrderBy(p => p.Id).Select(p => p.Name);
        }

        public MatchResponse Post(Match request)
        {
            request.DateTime = DateTime.Now;
            request.UserAuthId = UserSession.UserAuthId;

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request, true);
                
                foreach (var teamMatch in request.TeamMatches)
                {
                    teamMatch.Team = TryGetExistingTeam(db, teamMatch);
                    db.Save(teamMatch.Team);
                    teamMatch.TeamId = teamMatch.Team.Id;
                    db.Save(teamMatch, true);

                    foreach (var playerMatch in teamMatch.PlayerMatches)
                    {
                        playerMatch.Player = TryGetExistingPlayer(db, playerMatch);
                        db.Save(playerMatch.Player);
                        playerMatch.PlayerId = playerMatch.Player.Id;
                        db.Save(playerMatch, true);
                    }
                }
            }

            return Get(request);
        }

        private Team TryGetExistingTeam(IDbConnection db, TeamMatch teamMatch)
        {
            return db.Select<Team>(t => t.Name == teamMatch.Team.Name).FirstOrDefault() ?? teamMatch.Team;
        }

        private Player TryGetExistingPlayer(IDbConnection db, PlayerMatch playerMatch)
        {
            return db.Select<Player>(p => p.Name == playerMatch.Player.Name).FirstOrDefault() ?? playerMatch.Player;
        }
    }
}
