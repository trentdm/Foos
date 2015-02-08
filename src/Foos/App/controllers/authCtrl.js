app.controller('AuthCtrl', ['$rootScope', '$scope', 'authService', function($rootScope, $scope, authService) {
    $scope.register = function (name, pass, email) {
        var regSuccess = function(data, status, headers, config) {
            if (status == 200) {
                authService.user = {
                    name: data.userName,
                    userId: data.userId,
                    sessionId: data.sessionId,
                    isAuthenticated: true
                };
                
                $rootScope.$broadcast('alert', { type: 'success', message: 'Welcome, ' + data.userName });
            } else {
                regError(data, status, headers, config);
            }
        };
        
        var regError = function(data, status, headers, config) {
            authService.user = {
                name: undefined,
                isAuthenticated: false
            };
            
            $rootScope.$broadcast('alert', { type: 'warning', msg: 'Failed to register. ' + data.responseStatus.message });
        };
        
        authService.register(name, pass, email)
            .success(regSuccess)
            .error(regError);
        $scope.$close(authService.user);
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