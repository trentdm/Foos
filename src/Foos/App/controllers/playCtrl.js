angular.module('app').controller('PlayCtrl',
    ['$scope', 'matchService', function ($scope, matchService) {
        $scope.match = {
            dateTime: new Date().toLocaleString(),
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

    $scope.canSubmitMatch = false;

    $scope.addPoint = function(team, user) {
        if (team.score < 8 && user.points < 8) {
            team.score++;
            user.points++;
        }

        updateMatch();
    };

    $scope.subtractPoint = function (team, user) {
        if (team.score > 0 && user.points > 0) {
            team.score--;
            user.points--;
        }

        updateMatch();
    };

    updateMatch = function () {
        var winnerCount = 0;

        $scope.match.teams.forEach(function(team) {
            team.isWinner = team.score == 8;

            if (team.isWinner)
                winnerCount++;
        });
        $scope.canSubmitMatch = winnerCount == 1;
    };

    $scope.submitMatch = function (match) {
        matchService.submitMatch(match).then(function(result) {
            //todo:notify ui scores accepted
            //ask if they want to play again and reset if so
        });
    }
}]);