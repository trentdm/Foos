app.service('playermatchService', ['$http', function ($http) {
    var getClientFormattedMatches = function (matches) {
        return matches.map(function (match) {
            return {
                dateTime: new Date(parseInt(match.dateTime.substr(6))).toLocaleString(),
                teams: match.teamMatches.map(function(teamMatch) {
                    return {
                        name: teamMatch.team.name,
                        score: teamMatch.score,
                        isWinner: teamMatch.isWinner,
                        players: teamMatch.playerMatches.map(function(playerMatch) {
                            return {
                                name: playerMatch.player.name,
                                points: playerMatch.points,
                                position: { id: playerMatch.positionId }
                            };
                        })
                    };
                })
            };
        });
    };

    this.getMatches = function (successCallback, errorCallback, playerName) {
        $http.get('/api/match/playerMatches/' + playerName)
            .success(function(data) {
                successCallback(getClientFormattedMatches(data.results));
            })
            .error(function(data, status) {
                errorCallback(data, status);
            });
    };
}]);