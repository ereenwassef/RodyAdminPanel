/// <reference path="angular.js" />

var app = angular.module("myApp", ["ngRoute"]);


app.config(["$routeProvider", function ($routeProvider) {

    $routeProvider.
    when('/page_1', {
        templateUrl: 'page_1',
        controller: 'ListController' 
    }).
     when('/page_2', {
         templateUrl: 'page_2',
         controller: 'DetailsController'
     }).
    otherwise({
        redirectTo: '/page_1'
    });

}]);

//===========================================================
app.controller("ListController", function ($scope) {

    $scope.name = "yoyaaa  mohammed";

});


app.controller("DetailsController", function ($scope) {

    $scope.details = "web developer";

});
