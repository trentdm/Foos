app.service('playService', ['matchService', 'teamService', 'playerService', 'positionService', function(matchService, teamService, playerService, positionService) {
    this.getFreshMatch = function() {
        return {
            teams: [
                {
                    name: "",
                    score: 0,
                    players: [
                        { name: "", points: 0, position: { id: 0 } },
                        { name: "", points: 0, position: { id: 1 } }
                    ]
                },
                {
                    name: "",
                    score: 0,
                    players: [
                        { name: "", points: 0, position: { id: 0 } },
                        { name: "", points: 0, position: { id: 1 } }
                    ]
                }
            ]
        };
    };

    this.submitMatch = function(match, successCallback, errorCallback) {
        matchService.submitMatch(match, successCallback, errorCallback);
    };

    this.getTeamNames = function(successCallBack) {
        teamService.getTeams(function(response) {
            var data = response.results.map(function(t) {
                return t.name;
            });
            successCallBack(data);
        });
    };

    this.getPlayerNames = function(successCallBack) {
        playerService.getPlayers(function(response) {
            var data = response.results.map(function(p) {
                return p.name;
            });
            successCallBack(data);
        });
    };

    this.getPositions = function(successCallback) {
        positionService.getPositions(function(response, status, headers, config) {
            successCallback(response.results, status, headers, config);
        });
    }
}]);