using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/match")]
    [Route("/api/match/{id}")]
    public class Match
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string DateTime { get; set; }
        [Reference]
        public List<Team> Teams { get; set; }
    }

    public class MatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Match> Results { get; set; }
    }
}
