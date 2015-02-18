using System.Linq;
using Foos.Api.Operations;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Api.Services
{
    public class PreferencesService : Service
    {
        private IDbConnectionFactory DbConnectionFactory { get; set; }

        private AuthUserSession Session
        {
            get
            {
                try { return base.SessionAs<AuthUserSession>(); }
                catch { return new AuthUserSession(); } //fallback for unittesting
            }
        }

        public PreferencesService(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public PreferencesResponse Get(Preferences request)
        {
            ValidateUserRequest(request);

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                var userPrefs = db.LoadSelect<Preferences>(p => p.UserId == request.UserId);
                var userPref = userPrefs.Count == 1 ? userPrefs.First()
                    : new Preferences {UserId = request.UserId, UserName = Session.UserAuthName};
                return new PreferencesResponse { Result = userPref };
            }
        }

        public PreferencesResponse Post(Preferences request)
        {
            ValidateUserRequest(request);

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request);
                return Get(request);
            }
        }

        public PreferencesResponse Put(Preferences request)
        {
            ValidateUserRequest(request);

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.Save(request);
                return Get(request);
            }
        }

        private void ValidateUserRequest(Preferences request)
        {
            if (request.UserId != Session.UserAuthId)
                throw new HttpError("Unauthorized user.");
        }
    }
}
