angular.module('app').controller('PlayerCtrl',
    ['$scope', 'playerService', function ($scope, playerService) {
        playerService.getPlayers().then(function (players) {
            $scope.players = players.data.Results;
        });
}]);