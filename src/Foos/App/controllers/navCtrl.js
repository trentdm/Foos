angular.module('app').controller('NavCtrl',
    ['$scope', '$state', 'authModal', 'authService',
        function($scope, $state, authModal, authService) {
            $scope.isActive = function(tab) {
                return tab == $state.current.name;
            };

            $scope.user = authService.user;

            $scope.signin = function() {
                authModal();
            };

            $scope.signout = function() {
                authService.signout();
            };
        }]);