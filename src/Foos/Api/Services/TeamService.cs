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
                return new TeamResponse {Total = teams.Count, Results = teams};
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
