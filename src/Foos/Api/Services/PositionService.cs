using Foos.Api.Operations;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Api.Services
{
    public class PositionService : Service
    {
        private IDbConnectionFactory DbConnectionFactory { get; set; }

        public PositionService(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public PositionResponse Get(Position request)
        {
            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var players = request.Id == 0 ? db.LoadSelect<Position>() : db.LoadSelect<Position>(t => t.Id == request.Id);
                return new PositionResponse {Total = players.Count, Results = players};
            }
        }
    }
}
