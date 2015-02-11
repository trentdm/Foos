using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/player", "GET")]
    [Route("/api/player/{id}", "GET")]
    public class Player
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        public string Name { get; set; }
    }

    [Route("/api/playermatch", "GET")]
    [Route("/api/playermatch/{id}", "GET")]
    public class PlayerMatch
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int TeamMatchId { get; set; }
        [References(typeof(Player))]
        public int PlayerId { get; set; }
        [Ignore]
        public Player Player { get; set; }
        public int PositionId { get; set; }
        public int Points { get; set; }
    }

    public class PlayerResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Player> Results { get; set; }
    }

    public class PlayerMatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<PlayerMatch> Results { get; set; }
    }
}
