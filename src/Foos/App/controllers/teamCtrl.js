app.controller('TeamCtrl', ['$rootScope', '$scope', 'teamService', 'pageService', function ($rootScope, $scope, teamService, pageService) {
    var successCallback = function (teams) {
        $scope.teams = teams.results;
        $scope.pages = pageService.getPages($scope, teams.results, 10);
    };

    var errorCallback = function (data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    teamService.getTeams(successCallback, errorCallback);
}]);