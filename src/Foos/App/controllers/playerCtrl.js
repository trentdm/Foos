app.controller('PlayerCtrl', ['$scope', 'playerService', 'pageService', 'playermatchModal', function($scope, playerService, pageService, playermatchModal) {
    var successCallback = function (players) {
        $scope.players = players.results;
        $scope.pages = pageService.getPages($scope, players.results, $scope.user.preferences.playersPerPage);
        var headerMap = [
            { 'name': 'Name', 'field': 'name' },
            { 'name': 'Wins', 'field': 'wins' },
            { 'name': 'Losses', 'field': 'losses' },
            { 'name': 'Games', 'field': 'games' },
            { 'name': 'Avg', 'field': 'winAvg' },
            { 'name': 'Points', 'field': 'points' },
            { 'name': 'Avg', 'field': 'pointsAvg' }
        ];
        $scope.sort = pageService.getSort(headerMap, undefined);
    };

    var errorCallback = function (data) {
        if (data.responseMessage)
            $scope.$emit('alert', { type: 'danger', msg: data.responseStatus.message });
        else
            $scope.$emit('alert', { type: 'warning', msg: 'Server error, please try again later.' });
    };

    playerService.getPlayers(successCallback, errorCallback);

    $scope.displayPlayerMatch = function (player) {
        playermatchModal(player);
    }
}]);