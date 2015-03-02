app.service('playermatchModal', ['$modal', function ($modal) {
    return function (player) {
        var instance = $modal.open({
            templateUrl: 'App/partials/playermatch.html',
            controller: 'PlayermatchCtrl',
            controllerAs: 'PlayermatchCtrl',
            resolve: {
                playerName: function () {
                    return player.name;
                }
            }
        });

        return instance.result;
    };
}]);