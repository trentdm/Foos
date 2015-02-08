app.service('authService', [function() {
    this.user = { name: undefined, isAuthenticated: false };

    //todo: actual authentication, local persistence
    this.signin = function(name, pass) {
        this.user.name = name;
        this.user.isAuthenticated = true;
    };

    this.signout = function() {
        if (this.user.isAuthenticated) {
            this.user.name = undefined;
            this.user.isAuthenticated = false;
        }
    };
}]);