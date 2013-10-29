var controllers = angular.module('DQCSApp.controllers', []);

controllers.controller("CreateCtrl", function ($scope, $location, DQCS) {
    $scope.OfpSystems = DQCS.queryOfpSystem();
    $scope.Types = DQCS.queryTypes();
    $scope.IsImplementedList = [{ Id: 1, Label: "Yes" }, { Id: 0, Label: "No" }];
    $scope.save = function () {
        DQCS.save($scope.item, function () {
            $location.path('/');
        }, function () {
            console.log("insert failed");
        });
    };
});



controllers.controller("EditCtrl", function ($scope, $routeParams, $location, DQCS) {
    $scope.OfpSystems = DQCS.queryOfpSystem();
    $scope.Types = DQCS.queryTypes();
    $scope.IsImplementedList = [{ Id: 1, Label: "Yes" }, { Id: 0, Label: "No" }];
    $scope.item = DQCS.get({ id: $routeParams.itemId });

    $scope.save = function () {
        DQCS.update({ id: $scope.item.Id }, $scope.item, function () {
            $location.path('/');
        });
    }
});

controllers.controller("ListCtrl", function ($scope, $location, DQCS) {
    $scope.search = function () {
        DQCS.query({ q: $scope.query, systemQuery: $scope.systemQuery, isImplementedQuery: $scope.isImplementedQuery,typeQuery : $scope.typeQuery, limit: $scope.limit, offset: $scope.offset },
            function (items) {
                var cnt = items.length;
                $scope.no_more = cnt < $scope.limit;
                $scope.items = $scope.items.concat(items);
            }
        )
    };

    

    $scope.delete = function (itemId) {
        DQCS.delete({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
            toastr.success("Check List element deleted!");
        });
    }

    $scope.askDelete = function () {
        var itemId = this.item.Id;
        bootbox.confirm("Confirm delete?", function (result) {
            if (result === true) {
                $scope.delete(itemId);
                $scope.$apply();
            }
        });
    }

    $scope.reset = function () {
        $scope.offset = 0;
        $scope.items = [];
        $scope.search();
    };

    $scope.showResetButton = function () {
        if (undefined != $scope.systemQuery && $scope.systemQuery.length > 0) {
            return true;
        }
        if (undefined != $scope.typeQuery && $scope.typeQuery.length > 0) {
            return true;
        }
        if (undefined != $scope.query && $scope.query.length > 0) {
            return true;
        }
        if (undefined != $scope.isImplementedQuery && $scope.isImplementedQuery.length > 0) {
            return true;
        }
        return false;
    }

    $scope.getMore = function () {
        $scope.offset = $scope.offset + $scope.limit;
        $scope.search();
    };

    $scope.do_show_arrow = function (col, ascending) {
        return ($scope.asc === ascending && col === $scope.sort_by);
    };

    $scope.OfpSystems = DQCS.queryOfpSystem();
    $scope.Types = DQCS.queryTypes();

    $scope.show_more = function () { return !$scope.no_more };
    $scope.limit = 10;
    $scope.offset = 0;
    $scope.reset();
    $scope.asc = true;
    $scope.sort_by = 'Id';
   
});