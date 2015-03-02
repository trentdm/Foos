app.controller('PlayermatchCtrl', ['$rootScope', '$scope', 'playermatchService', 'pageService', 'playerName', function ($rootScope, $scope, playermatchService, pageService, playerName) {
    var successCallback = function (matches) {
        $scope.matches = matches;
        var headerMap = [
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

    playermatchService.getMatches(successCallback, errorCallback, playerName);
}]);