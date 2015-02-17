app.controller('PlayerCtrl', ['$rootScope', '$scope', 'playerService', 'pageService', function($rootScope, $scope, playerService, pageService) {
    var successCallback = function (players) {
        $scope.players = players.results;
        $scope.pages = pageService.getPages($scope, players.results, 10);
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
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    playerService.getPlayers(successCallback, errorCallback);
}]);