using Foos.Api.Operations;
using ServiceStack;

namespace Foos.Api.Services
{
    public class HelloService : Service
    {
        public HelloResponse Any(Hello request)
        {
            var name = string.IsNullOrEmpty(request.Name) ? "world" : request.Name;
            return new HelloResponse { Result = "Hello, " + name };
        }
    }
}
