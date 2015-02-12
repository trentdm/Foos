app.service('authService', ['$http', '$cookieStore', function ($http, $cookieStore) {
    var cookie = $cookieStore.get('user');
    if (cookie) {
        this.user = cookie;
    } else {
        this.user = { name: undefined, isAuthenticated: false };
    }

    this.register = function(name, pass, email, successCallback, errorCallback) {
        $http.post('/register', {
            userName: name,
            password: pass,
            email: email,
            autoLogin: true,
            rememberMe: true
        })
        .success(function(data, status, headers, config) {
                $cookieStore.put('user', { name: name, isAuthenticated: true });
                successCallback(data, status, headers, config);
            })
        .error(function(data, status, headers, config) {
            errorCallback(data, status, headers, config);
        });
    };

    this.signin = function(name, pass,successCallback, errorCallback) {
        $http.post('/auth/credentials', {
            userName: name,
            password: pass,
            rememberMe: true
        })
        .success(function(data, status, headers, config) {
            $cookieStore.put('user', { name: name, isAuthenticated: true });
            successCallback(data, status, headers, config);
        })
        .error(function(data, status, headers, config) {
            errorCallback(data, status, headers, config);
        });
    };

    this.signout = function() {
        if (this.user.isAuthenticated) {
            this.user.name = undefined;
            this.userId = undefined;
            this.sessionId = undefined;
            this.user.isAuthenticated = false;
            $cookieStore.remove('user');
        }
        return this.user;
    };
}]);