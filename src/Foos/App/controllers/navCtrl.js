angular.module('app').controller('NavCtrl',
    ['$scope', '$state', 'authModal', 'authService',
        function ($scope, $state, authModal, authService) {
            $scope.isActive = function (tab) {
                return tab == $state.current.name;
            };

            $scope.user = authService.user;
            
            $scope.signin = function () {
                authModal();

                //for if I want to do routing after nav signin, 
                //  currently just maintaining state
                //    .then(function () {
                //    return $state.go('play');
                //})
                //    .catch(function() {
                //        return $state.go('home');
                //    });
            };

            $scope.signout = function() {
                authService.signout();
            };
        }]);