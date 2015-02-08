app.controller('AuthCtrl', ['$scope', 'authService', function($scope, authService) {
    $scope.register = function(name, pass, email) {
        var user = authService.register(name, pass, email);
        $scope.$close(user);
    };

    $scope.signin = function(name, pass) {
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