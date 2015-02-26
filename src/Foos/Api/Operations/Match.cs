using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;

namespace Foos.Api.Operations
{
    [Route("/api/match", "GET POST")]
    [Route("/api/match/{id}", "GET")]
    [Authenticate(ApplyTo.Post | ApplyTo.Put | ApplyTo.Delete)] 
    public class Match
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        [Reference]
        public List<TeamMatch> TeamMatches { get; set; }
        public string UserAuthId { get; set; }
        [Ignore]
        public string UserAuthName { get; set; }
    }

    [Route("/api/match/playerMatches/{playerName}", "GET")]
    public class SearchPlayerGames
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        [Reference]
        public List<TeamMatch> TeamMatches { get; set; }
        public string UserAuthId { get; set; }
        [Ignore]
        public string UserAuthName { get; set; }
        public string PlayerName { get; set; }
    }

    public class MatchValidator : AbstractValidator<Match>
    {
        public TeamValidator TeamValidator { get; set; }

        public MatchValidator()
        {
            RuleSet(ApplyTo.Post | ApplyTo.Put, () =>
            {
                RuleFor(r => r.TeamMatches).Must(tm => tm.Count == 2);
                RuleFor(r => r.TeamMatches).Must(tm => tm.TrueForAll(t => t.PlayerMatches.Count > 0));
                RuleFor(r => r.TeamMatches).Must(tm => tm.TrueForAll(t => t.PlayerMatches.TrueForAll(p => !string.IsNullOrWhiteSpace(p.Player.Name))));
            });
        }
    }
    
    public class MatchResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Match> Results { get; set; }
    }

    public class SearchPlayerGamesResponse : ResponseStatus
    {
        public int Total { get; set; }
        public List<Match> Results { get; set; }
    }
}
