app.controller('TeamCtrl', ['$scope', 'teamService', 'pageService', function ($scope, teamService, pageService) {
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
        if (data.responseMessage)
            $scope.$emit('alert', { type: 'danger', msg: data.responseStatus.message })
        else
            $scope.$emit('alert', { type: 'warning', msg: 'Server error, please try again later.' });
    };

    teamService.getTeams(successCallback, errorCallback);
}]);