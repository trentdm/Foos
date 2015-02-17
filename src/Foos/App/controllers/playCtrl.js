app.controller('PlayCtrl', ['$scope', 'playService', function($scope, playService) {
    $scope.match = playService.getFreshMatch();
    playService.getPlayerNames(function(data) { $scope.playerNames = data; });
    $scope.canSubmitMatch = false;

    $scope.addPoint = function(team, player) {
        if (team.score < 8 && player.points < 8) {
            team.score++;
            player.points++;
            updateMatch();
        }
    };

    $scope.subtractPoint = function (team, player) {
        if (team.score > 0 && player.points > 0) {
            team.score--;
            player.points--;
            updateMatch();
        }
    };

    var updateMatch = function() {
        var winnerCount = 0;

        $scope.match.teams.forEach(function(team) {
            team.isWinner = team.score == 8;

            if (team.isWinner)
                winnerCount++;
        });
        $scope.canSubmitMatch = winnerCount == 1;
    };
    
    var submitSuccess = function (data, status, headers, config) {
        if (status == 200) {
            $scope.match = playService.getFreshMatch();
            updateMatch();
            $scope.$emit('alert', { type: 'success', msg: 'Match submitted, thanks!' });
        } else {
            submitError(data, status, headers, config);
        }
    };

    var submitError = function (data, status, headers, config) {
        if(data.responseMessage)
            $scope.$emit('alert', { type: 'warning', msg: 'Submission error. ' + data.responseStatus.message });
        else
            $scope.$emit('alert', { type: 'warning', msg: 'Server error, please try again later.' });
    };

    $scope.submitMatch = function(match) {
        playService.submitMatch(match, submitSuccess, submitError);
    };
}]);