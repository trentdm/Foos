app.controller('PlayerCtrl', ['$rootScope', '$scope', 'playerService', function($rootScope, $scope, playerService) {
    var successCallback = function (players) {
        $scope.players = players.results;
    };

    var errorCallback = function (data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    playerService.getPlayers(successCallback, errorCallback);
}]);