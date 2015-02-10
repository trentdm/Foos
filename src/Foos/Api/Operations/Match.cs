using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/match", "GET POST")]
    [Route("/api/match/{id}", "GET")]
    [Authenticate(ApplyTo.Post | ApplyTo.Put | ApplyTo.Delete)] 
    public class Match
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        [Reference]
        public List<TeamMatch> TeamMatches { get; set; }
        public string UserAuthId { get; set; }
    }
    
    public class MatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Match> Results { get; set; }
    }
}
