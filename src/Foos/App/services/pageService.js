app.service('pageService', [function() {
    this.getPages = function($scope, items, itemsPerPage) {
        var pages = {
            currentPage: 1,
            totalItems: items.length,
            itemsPerPage: itemsPerPage,
            pageCount: Math.ceil(items.length / itemsPerPage),
            doShow: items.length > itemsPerPage,
            menuSize: 7
        };

        var watchPagination = function () {
            var begin = ((pages.currentPage - 1) * pages.itemsPerPage);
            var end = begin + pages.itemsPerPage;
            pages.items = items.slice(begin, end);
            pages.doShow = items.length > itemsPerPage;
        }

        $scope.$watch('query', function (queryVal) {
            if (queryVal != undefined && queryVal != '') {
                pages.items = items;
                pages.doShow = false;
            } else {
                watchPagination();
            }
        });

        $scope.$watch('pages.currentPage + pages.itemsPerPage', function () {
            watchPagination();
        });

        return pages;
    };

    this.getSort = function(headerMap, defaultOrderBy) {
        return {
            headerMap: headerMap,
            orderBy: defaultOrderBy,
            reverseSort: true,
            updateOrderBy: function(field) {
                if (this.orderBy == field) {
                    this.reverseSort = !this.reverseSort;
                }

                this.orderBy = field;
            }
        };
    }
}]);