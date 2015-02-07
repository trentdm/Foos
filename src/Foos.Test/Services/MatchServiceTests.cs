using System.Collections.Generic;
using System.Linq;
using Foos.Api.Operations;
using Foos.Api.Services;
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

        [TestInitialize]
        public void Setup()
        {
            DbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            Service = new MatchService(DbConnectionFactory);

            using (var db = DbConnectionFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<Match>();
                db.DropAndCreateTable<Team>();
                db.DropAndCreateTable<Player>();
            }
        }

        [TestMethod]
        public void TestPost_DateTime()
        {
            var request = GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.DateTime, result.DateTime);
        }

        [TestMethod]
        public void TestPost_Team()
        {
            var request = GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.Teams.First().Name, result.Teams.First().Name);
        }

        [TestMethod]
        public void TestPost_Player1()
        {
            var request = GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.Teams.First().Players.First().Name, result.Teams.First().Players.First().Name);
        }

        [TestMethod]
        public void TestPost_Player2()
        {
            var request = GetStubMatch();
            Service.Post(request);
            var result = Service.Get(new Match()).Results.First();
            Assert.AreEqual(request.Teams.First().Players[1].Name, result.Teams.First().Players[1].Name);
        }

        private Match GetStubMatch()
        {
            return new Match
            {
                DateTime = "1234",
                Teams = new List<Team>
                {
                    new Team
                    {
                        Name = "Team 1",
                        IsWinner = true,
                        Players = new List<Player>
                        {
                            new Player
                            {
                                Name = "Player 1",
                                Points = 2
                            },
                            new Player
                            {
                                Name = "Player 2",
                                Points = 6
                            }
                        }
                    },
                    new Team
                    {
                        Name = "Team 2",
                        Players = new List<Player>
                        {
                            new Player
                            {
                                Name = "Player 3",
                                Points = 2
                            },
                            new Player
                            {
                                Name = "Player 4",
                                Points = 3
                            }
                        }
                    }
                }
            };
        }
    }
}
