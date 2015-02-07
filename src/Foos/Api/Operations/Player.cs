using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/player")]
    [Route("/api/player/{id}")]
    public class Player
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }

    public class PlayerResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Player> Results { get; set; }
    }
}
