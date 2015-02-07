angular.module('app')
    .service('authModal', function ($modal, $rootScope) {

        //from http://brewhouse.io/blog/2014/12/09/authentication-made-simple-in-single-page-angularjs-applications.html

        function assignCurrentUser(user) {
            $rootScope.user = user;
            return user;
        }

        return function () {
            var instance = $modal.open({
                templateUrl: 'App/partials/auth.html',
                controller: 'AuthCtrl',
                controllerAs: 'AuthCtrl',
                size: 'sm'
            });

            return instance.result.then(assignCurrentUser);
        };
    });