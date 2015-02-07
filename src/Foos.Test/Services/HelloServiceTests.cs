using Foos.Api.Operations;
using Foos.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foos.Test.Services
{
    [TestClass]
    public class HelloServiceTests
    {
        private HelloService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Service = new HelloService();
        }

        [TestMethod]
        public void TestAny()
        {
            var request = new Hello {};
            var result = Service.Any(request);
            Assert.AreEqual("Hello, world", result.Result);
        }
    }
}
