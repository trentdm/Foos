app.controller('TeamCtrl', ['$rootScope', '$scope', 'teamService', function($rootScope, $scope, teamService) {
    var successCallback = function (teams) {
        $scope.teams = teams.results;
    };

    var errorCallback = function (data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    teamService.getTeams(successCallback, errorCallback);
}]);