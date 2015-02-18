app.controller('PreferencesCtrl', ['$scope', 'preferencesService', function($scope, preferencesService) {
    var getSuccessCallback = function (result) {
        $scope.user.preferences = result;
    };

    var setSuccessCallback = function (result) {
        $scope.user.preferences = result;
        $scope.$emit('alert', { type: 'success', msg: 'Preferences updated!' });
    };

    var errorCallback = function (data) {
        if (data.responseMessage)
            $scope.$emit('alert', { type: 'danger', msg: data.responseStatus.message });
        else
            $scope.$emit('alert', { type: 'warning', msg: 'Server error, please try again later.' });
    };

    preferencesService.get($scope.user.id, getSuccessCallback, errorCallback);

    $scope.update = function(preferences) {
        preferencesService.set(preferences, setSuccessCallback, errorCallback);
    }
}]);