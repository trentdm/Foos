app.service('playService', ['matchService', 'teamService', 'playerService', function (matchService, teamService, playerService) {
    this.getFreshMatch = function () {
        return {
            teams: [
                {
                    name: "",
                    score: 0,
                    players: [
                        { name: "", points: 0 },
                        { name: "", points: 0 }
                    ]
                },
                {
                    name: "",
                    score: 0,
                    players: [
                        { name: "", points: 0 },
                        { name: "", points: 0 }
                    ]
                }
            ]
        };
    };

    this.submitMatch = function(match) {
        return matchService.submitMatch(match);
    }

    this.getTeamNames = function (successCallBack) {
        teamService.getTeams()
            .success(function(response) {
                var data = response.results.map(function(t) {
                        return t.name;
                    }
                );
            successCallBack(data);
        });
    };

    this.getPlayerNames = function (successCallBack) {
        playerService.getPlayers()
            .success(function (response) {
                var data = response.results.map(function (p) {
                    return p.name;
                });
            successCallBack(data);
        });
    };
}]);