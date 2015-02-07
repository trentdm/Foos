angular.module('app')
    .service('playerService', ['$http',function ($http) {
        this.getPlayers = function () {
            return $http.get('/api/player');
        };

        this.getPlayer = function (player) {
            return $http.get('/api/player/' + player.Id);
        };
    }]);