app.controller('HeadCtrl', ['$scope', '$state', '$timeout', 'authModal', 'authService', 'versionService', function($scope, $state, $timeout, authModal, authService, versionService) {
    $scope.alerts = [];

    $scope.addAlert = function(alert) {
        $scope.alerts.push(alert);
        $timeout(function() {
            $scope.alerts.splice($scope.alerts.indexOf(alert), 1);
        }, 8000);
    };

    $scope.$on('alert', function (event, alert) {
        $scope.addAlert(alert);
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

    versionService.getVersionInfo(
        function(version) {
            if (version.isOutOfDate) {
                $scope.addAlert({ type: 'warning', msg: 'Client version is out of date. Please refresh your browser.' });
            }
        },
        function() {
            $scope.addAlert({ type: 'danger', msg: 'Server could not be reached. Please try again later.' });
        });
}]);