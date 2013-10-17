var DQCSApp = angular.module("DQCSApp", ["ngRoute", "ngResource","ngAnimate", "DQCSApp.services", "DQCSApp.controllers","filters"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/edit/:itemId', { controller: 'EditCtrl', templateUrl: 'detail.html' }).
            when('/new', { controller: 'CreateCtrl', templateUrl: 'detail.html' }).
            when('/', { controller: 'ListCtrl', templateUrl: 'list.html' }).
            otherwise({ redirectTo: '/' });
    });