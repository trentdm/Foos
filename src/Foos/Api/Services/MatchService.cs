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
                    foreach (var team in match.Teams)
                        team.Players = db.Select<Player>(p => p.TeamId == team.Id);

                return new MatchResponse { Total = matches.Count, Results = matches };
            }
        }

        public MatchResponse Post(Match request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request, true);

                foreach (var team in request.Teams)
                    db.SaveReferences(team, team.Players);

                return Get(request);
            }
        }

        public MatchResponse Put(Match request)
        {
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
                var id = db.Delete(request);
                return new MatchResponse {Results = new List<Match> {request}};
            }
        }
    }
}
