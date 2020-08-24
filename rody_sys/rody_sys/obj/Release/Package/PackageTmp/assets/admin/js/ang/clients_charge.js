/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("ctr_clientC", function ($scope, $http) {

    $scope.get_all_governs = function () {
        $http({
            url: "../governs/get_all_govern",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allGovern = mydata.data.allGovern;
        });
    }
    $scope.get_all_governs();
    //------------------------------
    $scope.get_all_areas = function () {
        $http({
            url: "../areas/get_all_areas",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allArea = mydata.data.allArea;
        });
    }

    $scope.get_all_areas();

//=======================================================
    $scope.get_all=function()
    {


        var d = "";
        if ($scope.date !=null)
        {
            d = $scope.date;
        }



        $http({
            url: "../clients/get_clients_charge?date=" + d,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
        });
    }

    $scope.get_all(); // to get all ...
//=======================================================
    
   


}); //end controller