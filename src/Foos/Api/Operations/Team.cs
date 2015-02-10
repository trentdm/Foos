using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/team", "GET")]
    [Route("/api/team/{id}", "GET")]
    public class Team
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        public string Name { get; set; }
    }

    [Route("/api/teammatch", "GET")]
    [Route("/api/teammatch/{id}", "GET")]
    public class TeamMatch
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int MatchId { get; set; }
        [References(typeof(Team))]
        public Team Team { get; set; }
        public int Score { get; set; }
        public bool IsWinner { get; set; }
        [Reference]
        public List<PlayerMatch> PlayerMatches { get; set; } 
    }

    public class TeamResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Team> Results { get; set; }
    }

    public class TeamMatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<TeamMatch> Results { get; set; }
    }
}
