using System;
using System.Collections.Generic;
using Foos.Api.Operations;
using ServiceStack;
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
                    match.TeamMatches = db.LoadSelect<TeamMatch>();

                    foreach (var teamMatch in match.TeamMatches)
                    {
                        teamMatch.PlayerMatches = db.LoadSelect<PlayerMatch>();
                    }
                }

                return new MatchResponse { Total = matches.Count, Results = matches };
            }
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
                    db.Save(teamMatch, true);
                    db.Save(teamMatch.Team);

                    foreach (var playerMatch in teamMatch.PlayerMatches)
                    {
                        db.Save(playerMatch, true);
                        db.Save(playerMatch.Player);
                    }
                }
            }

            return Get(request);
        }

        public MatchResponse Put(Match request)
        {
            request.UserAuthId = UserSession.UserAuthId;
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var id = db.Update(request);
                return Get(new Match { Id = id });
            }
        }

        public MatchResponse Delete(Match request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Delete(request);
                return new MatchResponse {Results = new List<Match> {request}};
            }
        }
    }
}
