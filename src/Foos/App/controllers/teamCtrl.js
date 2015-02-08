angular.module('app').controller('TeamCtrl',
    ['$scope', 'teamService', function ($scope, teamService) {
        teamService.getTeams().then(function (teams) {
            $scope.teams = teams.data.results;
        });
}]);