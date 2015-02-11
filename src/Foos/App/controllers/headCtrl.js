app.controller('HeadCtrl', ['$scope', '$state', '$timeout', 'authModal', 'authService', function($scope, $state, $timeout, authModal, authService) {
    $scope.alerts = [];
    
    $scope.$on('alert', function (event, alert) {
        $scope.alerts.push(alert);
        $timeout(function () {
            $scope.alerts.splice($scope.alerts.indexOf(alert), 1);
        }, 5000);
    });

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.isActive = function (tab) {
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