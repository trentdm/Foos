app.controller('TeamCtrl', ['$rootScope', '$scope', 'teamService', function($rootScope, $scope, teamService) {
    var getMatchesSuccess = function (teams) {
        $scope.teams = teams.results;
    };

    var getMatchesError = function (data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    teamService.getTeams(getMatchesSuccess, getMatchesError);
}]);