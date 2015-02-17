app.controller('MatchCtrl', ['$scope', 'matchService', 'pageService', function($scope, matchService, pageService) {
    var successCallback = function (matches) {
        $scope.matches = matches;
        $scope.pages = pageService.getPages($scope, matches, 5);
        var headerMap = [
            { 'name': '#', 'field': 'id' },
            { 'name': 'Date', 'field': 'dateTime' }
        ];
        $scope.sort = pageService.getSort(headerMap, undefined);
    };

    var errorCallback = function (data) {
        if (data.responseMessage)
            $scope.$emit('alert', { type: 'danger', msg: data.responseStatus.message });
        else
            $scope.$emit('alert', { type: 'warning', msg: 'Server error, please try again later.' });
    };

    matchService.getMatches(successCallback, errorCallback);
}]);