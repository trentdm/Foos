angular.module('app').controller('AuthCtrl',
    ['$scope', 'authService', function ($scope, authService) {

    $scope.signin = function (name, pass) {
        var user = authService.signin(name, pass);
        $scope.$close(user);

        //todo: will use instead, when service is async
        //authService.signin(name, pass)
        //.then(function (user) { 
        //    $scope.$close(user);
        //});
    };

    $scope.cancel = $scope.$dismiss;
}]);