using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/match")]
    [Route("/api/match/{id}")]
    [Authenticate(ApplyTo.Post | ApplyTo.Put | ApplyTo.Delete)] 
    public class Match : RequestContext
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string DateTime { get; set; }
        [Reference]
        public List<Team> Teams { get; set; }
        public string UserAuthId { get; set; }
    }
    
    public class MatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Match> Results { get; set; }
    }
}
