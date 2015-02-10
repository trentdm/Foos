using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Foos.Api.Operations
{
    [Route("/api/position", "GET")]
    [Route("/api/position/{id}", "GET")]
    public class Position
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Index(Unique = true)]
        public string Name { get; set; }
    }

    public class PositionResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Position> Results { get; set; }
    }
}
