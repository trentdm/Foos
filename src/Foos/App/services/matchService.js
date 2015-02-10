app.service('matchService', ['$http', function($http) {
    var getServiceCompatibleMatch = function(match) {
        return {
            teamMatches: [
                {
                    score: match.teams[0].score,
                    isWinner: match.teams[0].isWinner,
                    team: { name: match.teams[0].name },
                    playerMatches: [
                        {
                            points: match.teams[0].players[0].points,
                            player: { name: match.teams[0].players[0].name }
                        },
                        {
                            points: match.teams[0].players[1].points,
                            player: { name: match.teams[0].players[1].name }
                        }
                    ]
                },
                {
                    score: match.teams[1].score,
                    isWinner: match.teams[1].isWinner,
                    team: { name: match.teams[1].name },
                    playerMatches: [
                        {
                            points: match.teams[1].players[0].points,
                            player: { name: match.teams[1].players[0].name }
                        },
                        {
                            points: match.teams[1].players[1].points,
                            player: { name: match.teams[1].players[1].name }
                        }
                    ]
                }
            ]
        }
    };

    this.submitMatch = function (match) {
        var serviceCompatibleMatch = getServiceCompatibleMatch(match);
        return $http.post('/api/match', serviceCompatibleMatch);
    };

    this.getMatches = function() {
        return $http.get('/api/match');
    };

    this.getMatch = function(match) {
        return $http.get('/api/match/' + match.Id);
    };
}]);