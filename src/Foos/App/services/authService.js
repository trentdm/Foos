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
        this.user.name = name;
        this.user.isAuthenticated = true;
        return this.user;
    };

    this.signout = function() {
        if (this.user.isAuthenticated) {
            this.user.name = undefined;
            this.user.isAuthenticated = false;
        }
        return this.user;
    };
}]);