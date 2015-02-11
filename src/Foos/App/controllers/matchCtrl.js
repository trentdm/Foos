app.controller('MatchCtrl', ['$rootScope', '$scope', 'matchService', function($rootScope, $scope, matchService) {
    var getMatchesSuccess = function(matches) {
        $scope.matches = matches;
    }

    var getMatchesError = function(data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    }

    matchService.getMatches(getMatchesSuccess, getMatchesError);
}]);