app.controller('MatchCtrl', ['$rootScope', '$scope', 'matchService', function($rootScope, $scope, matchService) {
    var successCallback = function(matches) {
        $scope.matches = matches;
    };

    var errorCallback = function(data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    matchService.getMatches(successCallback, errorCallback);
}]);