using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/preferences", "GET POST PUT")]
    [Route("/api/preferences/{userId}", "GET POST PUT")]
    [Authenticate] 
    public class Preferences
    {
        [PrimaryKey]
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Default(typeof(int), "5")]
        public int MatchesPerPage { get; set; }
        [Default(typeof(int), "10")]
        public int TeamsPerPage { get; set; }
        [Default(typeof(int), "10")]
        public int PlayersPerPage { get; set; }

        public Preferences()
        {
            MatchesPerPage = 5;
            TeamsPerPage = 10;
            PlayersPerPage = 10;
        }
    }

    public class PreferencesResponse : ResponseStatus
    {
        public Preferences Result { get; set; }
    }
}
