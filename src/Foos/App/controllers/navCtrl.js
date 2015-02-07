angular.module('app').controller('NavCtrl',
    ['$scope', '$state', 'authModal',
        function ($scope, $state, authModal) {
    $scope.openAuth = function () {
        authModal().then(function () {
            return $state.go('play', toParams);
        })
        .catch(function () {
            return $state.go('home');
        });
    }
}]);