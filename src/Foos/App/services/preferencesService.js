app.service('preferencesService', ['$http', function($http) {
    this.get = function(userId, successCallback, errorCallback) {
        $http.get('/api/preferences/' + userId)
            .success(function (data, status, headers, config) {
                if(status == 200)
                    successCallback(data.result);
                else
                    errorCallback(data, status);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };

    this.set = function(preferences, successCallback, errorCallback) {
        $http.put('/api/preferences/' + preferences.userId, preferences)
            .success(function(data, status, headers, config) {
                if (status == 200)
                    successCallback(data.result);
                else
                    errorCallback(data, status);
            })
            .error(function(data, status, headers, config) {
                errorCallback(data, status);
            });
    };
}]);