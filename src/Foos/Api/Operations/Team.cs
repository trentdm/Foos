using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/team")]
    [Route("/api/team/{id}")]
    public class Team
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsWinner { get; set; }
        [Reference]
        public List<Player> Players { get; set; }
    }

    public class TeamResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Team> Results { get; set; }
    }
}
