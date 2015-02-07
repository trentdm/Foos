angular.module('app')
    .service('authService', ['$rootScope',
        function ($rootScope) {
        this.signin = function (name, pass) {
            //todo: actual authentication, local persistence
                $rootScope.user = {
                    name: name,
                    isAuthenticated: true
                }
            return $rootScope.user;
        };

        this.signout = function (user) {
            if (user.isAuthenticated) {
                user.name = "";
                user.pass = "";
                user.isAuthenticated = false;
                $rootScope.user = user;
            }
            return user;
        };
    }]);