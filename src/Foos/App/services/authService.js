app.service('authService', ['$http', function($http) {
    this.user = { name: undefined, isAuthenticated: false };

    this.register = function(name, pass, email) {
        return $http.post('/register', {
            userName: name,
            password: pass,
            email: email,
            autoLogin: true
        });
    };

    this.signin = function(name, pass) {
        return $http.post('/auth/credentials', {
            userName: name,
            password: pass,
            //rememberMe: true
        });
    };

    this.signout = function() {
        if (this.user.isAuthenticated) {
            this.user.name = undefined;
            this.userId = undefined;
            this.sessionId = undefined;
            this.user.isAuthenticated = false;
        }
        return this.user;
    };
}]);