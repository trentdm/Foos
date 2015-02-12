using Foos.Api.Operations;
using ServiceStack;

namespace Foos.Api.Services
{
    public class VersionService : Service
    {
        public VersionResponse Get(Version request)
        {
            return new VersionResponse {Result = new Version()};
        }
    }
}
