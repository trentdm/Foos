using System.Collections.Generic;
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
                db.SaveAllReferences(request);
                return Get(request);
            }
        }

        public TeamResponse Put(Team request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var id = db.Update(request);
                return Get(new Team { Id = id });
            }
        }

        public TeamResponse Delete(Team request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var id = db.Delete(request);
                return new TeamResponse {Results = new List<Team> {request}};
            }
        }
    }
}
