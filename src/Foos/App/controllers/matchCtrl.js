app.controller('MatchCtrl', ['$rootScope', '$scope', 'matchService', 'pageService', function($rootScope, $scope, matchService, pageService) {
    var successCallback = function (matches) {
        $scope.matches = matches;
        $scope.pages = pageService.getPages($scope, matches, 5);
        var headerMap = [
            { 'name': '#', 'field': 'id' },
            { 'name': 'Date', 'field': 'dateTime' }
        ];
        $scope.sort = pageService.getSort(headerMap, 'name');
    };

    var errorCallback = function(data) {
        $rootScope.$broadcast('alert', { type: 'danger', msg: data.responseStatus.message });
    };

    matchService.getMatches(successCallback, errorCallback);
}]);