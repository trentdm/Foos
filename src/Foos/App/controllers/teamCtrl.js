app.controller('TeamCtrl', ['$rootScope', '$scope', 'teamService', 'pageService', function ($rootScope, $scope, teamService, pageService) {
    var successCallback = function (teams) {
        $scope.teams = teams.results;
        $scope.pages = pageService.getPages($scope, teams.results, 10);
        var headerMap = [
            { 'name': 'Players', 'field': 'name' },
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

    teamService.getTeams(successCallback, errorCallback);
}]);