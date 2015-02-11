app.service('teamService', ['$http', function($http) {
    this.getTeams = function (successCallback, errorCallback) {
        $http.get('/api/team')
            .success(function(data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };

    this.getTeam = function (team, successCallback, errorCallback) {
        $http.get('/api/team/' + team.Id)
            .success(function(data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };
}]);