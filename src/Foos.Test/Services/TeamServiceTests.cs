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
    public class TeamServiceTests
    {
        private TeamService Service { get; set; }
        private IDbConnectionFactory DbConnectionFactory { get; set; }
        private ITestHelper TestHelper { get; set; }

        [TestInitialize]
        public void Setup()
        {
            DbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            TestHelper = new TestHelper();
            TestHelper.SetupTestDb(DbConnectionFactory);
            Service = new TeamService(DbConnectionFactory);
        }

        [TestMethod]
        public void TestTeam_Post_Name()
        {
            var request = TestHelper.GetStubTeam();
            Service.Post(request);
            var result = Service.Get(new Team()).Results.First();
            Assert.AreEqual(request.Name, result.Name);
        }

        [TestMethod]
        public void TestTeamMatch_Post_Score()
        {
            var request = TestHelper.GetStubTeamMatch();
            Service.Post(request);
            var result = Service.Get(new TeamMatch()).Results.First();
            Assert.AreEqual(request.Score, result.Score);
        }
    }
}
