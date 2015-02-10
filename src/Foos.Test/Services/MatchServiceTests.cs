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
    public class MatchServiceTests
    {
        private MatchService Service { get; set; }
        private IDbConnectionFactory DbConnectionFactory { get; set; }
        private ITestHelper TestHelper { get; set; }

        [TestInitialize]
        public void Setup()
        {
            DbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            TestHelper = new TestHelper();
            TestHelper.SetupTestDb(DbConnectionFactory);
            Service = new MatchService(DbConnectionFactory);
        }

        [TestMethod]
        public void TestPost_DateTime()
        {
            var request = TestHelper.GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.DateTime, result.DateTime);
        }

        [TestMethod]
        public void TestPost_TeamMatch()
        {
            var request = TestHelper.GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().Score, result.TeamMatches.First().Score);
        }

        [TestMethod]
        public void TestPost_TeamMatch_Team()
        {
            var request = TestHelper.GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().Team.Name, result.TeamMatches.First().Team.Name);
        }

        [TestMethod]
        public void TestPost_TeamMatch_Team_NameConstraint()
        {
            var request = TestHelper.GetStubMatch();
            request.TeamMatches.ForEach(tm => tm.Team.Name = "team");
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().Team.Name, result.TeamMatches.First().Team.Name);
        }

        [TestMethod]
        public void TestPost_TeamMatch_PlayerMatch()
        {
            var request = TestHelper.GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().PlayerMatches.First().Points, result.TeamMatches.First().PlayerMatches.First().Points);
        }

        [TestMethod]
        public void TestPost_TeamMatch_PlayerMatch_Player()
        {
            var request = TestHelper.GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().PlayerMatches.First().Player.Name, result.TeamMatches.First().PlayerMatches.First().Player.Name);
        }

        [TestMethod]
        public void TestPost_TeamMatch_PlayerMatch_Player_NameConstraint()
        {
            var request = TestHelper.GetStubMatch();
            request.TeamMatches.ForEach(tm => tm.PlayerMatches.ForEach(pm => pm.Player.Name = "player"));
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.TeamMatches.First().PlayerMatches.First().Player.Name, result.TeamMatches.First().PlayerMatches.First().Player.Name);
        }
    }
}
