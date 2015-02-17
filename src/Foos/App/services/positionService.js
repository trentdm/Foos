app.service('positionService', ['$http', function($http) {
    this.getPositions = function(successCallback, errorCallback) {
        $http.get('/api/position')
            .success(function (data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };

    this.getPosition = function(player, successCallback, errorCallback) {
        $http.get('/api/position/' + player.Id)
            .success(function(data, status, headers, config) {
                successCallback(data);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };
}]);