app.service('playerService', ['$http', function($http) {
    this.getPlayers = function(successCallback, errorCallback) {
        $http.get('/api/player')
            .success(function(data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };

    this.getPlayer = function(player, successCallback, errorCallback) {
        $http.get('/api/player/' + player.Id)
            .success(function(data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };
}]);