using System;
using System.Collections.Generic;
using Foos.Api.Operations;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Foos.Test.Helpers
{
    public interface ITestHelper
    {
        void SetupTestDb(IDbConnectionFactory dbConnection);
        Match GetStubMatch();
        TeamMatch GetStubTeamMatch();
        Team GetStubTeam();
        PlayerMatch GetStubPlayerMatch();
        Player GetStubPlayer();
    }

    public class TestHelper : ITestHelper
    {
        public void SetupTestDb(IDbConnectionFactory dbConnection)
        {
            using (var db = dbConnection.OpenDbConnection())
            {
                db.CreateTableIfNotExists<Match>();
                db.CreateTableIfNotExists<Team>();
                db.CreateTableIfNotExists<TeamMatch>();
                db.CreateTableIfNotExists<Player>();
                db.CreateTableIfNotExists<PlayerMatch>();

                if (!db.TableExists<Position>())
                {
                    db.CreateTable<Position>();
                    db.InsertAll(new List<Position> { new Position { Id = 0, Name = "Unspecified" }, new Position { Id = 1, Name = "Front" }, new Position { Id = 2, Name = "Back" }, new Position { Id = 3, Name = "Solo" } });
                }
            }
        }

        public Match GetStubMatch()
        {
            return new Match
            {
                DateTime = DateTime.Now,
                TeamMatches = new List<TeamMatch>
                {
                    new TeamMatch
                    {
                        Score = 4,
                        IsWinner = false,
                        Team = new Team{Name = "Team1"},
                        PlayerMatches = new List<PlayerMatch>
                        {
                            new PlayerMatch
                            {
                                Points = 1, PositionId = 1,
                                Player = new Player{Name="Player1"}
                            },
                            new PlayerMatch
                            {
                                Points = 3, PositionId = 2,
                                Player = new Player{Name="Player2"}
                            }
                        }
                    },
                    new TeamMatch
                    {
                        Score = 8,
                        IsWinner = true,
                        Team = new Team{Name = "Team2"},
                        PlayerMatches = new List<PlayerMatch>
                        {
                            new PlayerMatch
                            {
                                Points = 5, PositionId = 1,
                                Player = new Player{Name="Player3"}
                            },
                            new PlayerMatch
                            {
                                Points = 3, PositionId = 2,
                                Player = new Player{Name="Player4"}
                            }
                        }
                    }
                }
            };
        }

        public TeamMatch GetStubTeamMatch()
        {
            return new TeamMatch
            {
                Score = 8,
                IsWinner = true,
                Team = new Team{Name = "Team2"},
                PlayerMatches = new List<PlayerMatch>
                {
                    new PlayerMatch
                    {
                        Points = 5, PositionId = 1,
                        Player = new Player{Name="Player3"}
                    },
                    new PlayerMatch
                    {
                        Points = 3, PositionId = 2,
                        Player = new Player{Name="Player4"}
                    }
                }
            };
        }

        public Team GetStubTeam()
        {
            return new Team {Name = "Team2"};
        }

        public PlayerMatch GetStubPlayerMatch()
        {
            return new PlayerMatch
            {
                Points = 5, PositionId = 1,
                Player = new Player{Name="Player3"}
            };
        }

        public Player GetStubPlayer()
        {
            return new Player {Name = "Player3"};
        }
    }
}