app.controller('PlayerCtrl', ['$rootScope', '$scope', 'playerService', 'pageService', function($rootScope, $scope, playerService, pageService) {
    var successCallback = function (players) {
        $scope.players = players.results;
        $scope.pages = pageService.getPages($scope, players.results, 10);
    };

    var errorCallback = function (data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    playerService.getPlayers(successCallback, errorCallback);
}]);