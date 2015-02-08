angular.module('app').controller('MatchCtrl',
    ['$scope', 'matchService', function ($scope, matchService) {
        matchService.getMatches().then(function(matches) {
            $scope.matches = matches.data.results;
        });
    }]);