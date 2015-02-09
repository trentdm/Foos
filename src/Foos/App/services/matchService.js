app.service('matchService', ['$http', 'authService', function($http, authService) {
    this.submitMatch = function(match) {
        return $http.post('/api/match', match);
    };

    this.getMatches = function() {
        return $http.get('/api/match');
    };

    this.getMatch = function(match) {
        return $http.get('/api/match/' + match.Id);
    };
}]);