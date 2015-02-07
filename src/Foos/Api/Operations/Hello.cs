using ServiceStack;

namespace Foos.Api.Operations
{
    [Route("/api/hello")]
    [Route("/api/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }

    public class HelloResponse : ResponseStatus
    {
        public string Result { get; set; }
    }
}
