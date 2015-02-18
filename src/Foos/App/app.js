'use strict';

var app = angular.module('app', [
    'ngAnimate',
    'ngCookies',
    'ui.router',
    'ui.bootstrap'
]);

app.config(['$stateProvider', '$httpProvider',
    function ($stateProvider, $httpProvider) {
        $stateProvider.state('home', {
            url: '',
            templateUrl: 'App/partials/home.html',
            controller: 'HomeCtrl',
            data: {
                requireLogin: false
            }
        });
        $stateProvider.state('play', {
            url: '/play',
            templateUrl: 'App/partials/play.html',
            controller: 'PlayCtrl',
            data: {
                requireLogin: true
            }
        });
        $stateProvider.state('match', {
            url: '/match',
            templateUrl: 'App/partials/match.html',
            controller: 'MatchCtrl',
            data: {
                requireLogin: false
            }
        });
        $stateProvider.state('team', {
            url: '/team',
            templateUrl: 'App/partials/team.html',
            controller: 'TeamCtrl',
            data: {
                requireLogin: false
            }
        });
        $stateProvider.state('player', {
            url: '/player',
            templateUrl: 'App/partials/player.html',
            controller: 'PlayerCtrl',
            data: {
                requireLogin: false
            }
        });
        $stateProvider.state('about', {
            url: '/about',
            templateUrl: 'App/partials/about.html',
            data: {
                requireLogin: false
            }
        });
        $stateProvider.state('preferences', {
            url: '/preferences',
            templateUrl: 'App/partials/preferences.html',
            controller: 'PreferencesCtrl',
            data: {
                requireLogin: true
            }
        });

        $httpProvider.interceptors.push(function ($timeout, $q, $injector) {
            var authModal, $http, $state;

            // this trick must be done so that we don't receive
            // `Uncaught Error: [$injector:cdep] Circular dependency found`
            $timeout(function () {
                authModal = $injector.get('authModal');
                $http = $injector.get('$http');
                $state = $injector.get('$state');
            });

            return {
                responseError: function (rejection) {
                    return rejection;//may want to force modal on 401 auth failure, but not at this time
                    if (rejection.status !== 401) {
                        return rejection;
                    }

                    var deferred = $q.defer();

                    authModal()
                      .then(function () {
                          deferred.resolve($http(rejection.config));
                      })
                      .catch(function () {
                          $state.go('home');
                          deferred.reject(rejection);
                      });

                    return deferred.promise;
                }
            };
        });
    }
]);

app.run(['$rootScope', '$state', 'authModal', 'authService', 'versionService',
    function ($rootScope, $state, authModal, authService, versionService) {
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
            versionService.getVersionInfo(
                function (version) {
                    if (version.isOutOfDate) {
                        $rootScope.$broadcast('alert', { type: 'warning', msg: 'Client version is out of date. Please refresh your browser.' });
                    }
                },
                function () {
                    $rootScope.$broadcast({ type: 'danger', msg: 'Server could not be reached. Please try again later.' });
                });

            if (toState.data.requireLogin && !authService.user.isAuthenticated) {
                event.preventDefault();

                authModal()
                    .then(function(data) {
                        return $state.go(toState.name, toParams);
                    })
                    .catch(function() {
                        return $state.go('home');
                    });
            }
        });
    }]);