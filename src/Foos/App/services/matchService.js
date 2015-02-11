app.service('matchService', ['$http', function($http) {
    var getServerFormattedMatch = function(match) {
        return {
            teamMatches: match.teams.map(function(team) {
                return {
                    score: team.score,
                    isWinner: team.isWinner,
                    team: { name: team.name },
                    playerMatches: team.players.map(function(player) {
                        return {
                            points: player.points,
                            player: { name: player.name }
                        };
                    })
                };
            })
        };
    };

    this.submitMatch = function (match, successCallback, errorCallback) {
        var serverFormattedMatch = getServerFormattedMatch(match);
        $http.post('/api/match', serverFormattedMatch)
            .success(function(data, status, headers, config) {
                successCallback(match, status);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data);
            });
    };

    var getClientFormattedMatches = function (matches) {
        return matches.map(function(match) {
            return {
                id: match.id,
                userAuthName: match.userAuthName,
                dateTime: new Date(parseInt(match.dateTime.substr(6))).toLocaleString(),
                teams: match.teamMatches.map(function(teamMatch) {
                    return {
                        name: teamMatch.team.name,
                        score: teamMatch.score,
                        isWinner: teamMatch.isWinner,
                        players: teamMatch.playerMatches.map(function(playerMatch) {
                            return {
                                name: playerMatch.player.name,
                                points: playerMatch.points
                            };
                        })
                    };
                })
            };
        });
    };

    this.getMatches = function (successCallback, errorCallback) {
        $http.get('/api/match')
            .success(function(data, status, headers, config) {
                successCallback(getClientFormattedMatches(data.results));
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };

    this.getMatch = function (match, successCallback, errorCallback) {
        $http.get('/api/match/' + match.Id)
            .success(function(data, status, headers, config) {
                successCallback(getClientFormattedMatches(data.results));
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };
}]);