using System.Linq;
using Foos.Api.Operations;
using Foos.Api.Services;
using Foos.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Test.Services
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService Service { get; set; }
        private IDbConnectionFactory DbConnectionFactory { get; set; }
        private ITestHelper TestHelper { get; set; }

        [TestInitialize]
        public void Setup()
        {
            DbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            TestHelper = new TestHelper();
            TestHelper.SetupTestDb(DbConnectionFactory);
            Service = new PlayerService(DbConnectionFactory);
        }

        [TestMethod]
        public void TestPlayerGet_Name()
        {
            var request = TestHelper.GetStubPlayer();
            Service.Post(request);
            var result = Service.Get(new Player()).Results.First();
            Assert.AreEqual(request.Name, result.Name);
        }

        [TestMethod]
        public void TestPlayerMatchGet_PlayerMatch()
        {
            var request = TestHelper.GetStubPlayerMatch();
            Service.Post(request);
            var result = Service.Get(new PlayerMatch()).Results.First();
            Assert.AreEqual(request.Points, result.Points);
        }
    }
}
