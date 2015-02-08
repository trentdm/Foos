app.service('teamService', ['$http', function($http) {
    this.getTeams = function() {
        return $http.get('/api/team');
    };

    this.getTeam = function(team) {
        return $http.get('/api/team/' + team.Id);
    };
}]);