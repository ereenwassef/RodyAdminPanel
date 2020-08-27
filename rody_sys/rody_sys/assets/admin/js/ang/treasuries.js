/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);
//var app = angular.module("myApp", []);

app.controller("treasuries", function ($scope, $http) {


    $scope.get_all_treasuries = function () {
        $http({
            url: "../treasuries/get_all_treasuries",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allTreasuries = mydata.data.allTreasuries;
        });
    }
    $scope.get_all_treasuries();
//==================================================
    var tId = 0;

    $scope.get_treasury = function (id) {
        
        tId = id;
    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../treasuries/delete_Treasury",
            method: "POST",
            data: { id: tId },
        }).then
        (function (mydata) {
            $scope.get_all_treasuries();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null || $scope.value==null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else
        {
            $http({
                url: "../treasuries/add_Treasury",
                method: "POST",
                data: { name: $scope.name, value: $scope.value },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.value=""
                $scope.get_all_treasuries();
                alert(mydata.data.msg);
            });
        }
    }
//===============================================      

    $scope.update=function(id,name,value)
    {
        tId = id;
        $scope.name2 = name;
      //  $scope.value2 = value;
    }

//===============================================
    $scope.updateDB = function () {

        if ($scope.name2 == null || $scope.value2 == null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../treasuries/update_Treasury",
                method: "POST",
                data: { id: tId, name: $scope.name2, value: $scope.value2, opType: document.getElementById("opTypeUpdate").value},
            }).then
            (function (mydata) {              
                $scope.get_all_treasuries();
                $scope.value2 = "";
                alert(mydata.data.msg);
            });
        }
    }

//================================================
    $scope.get_stores = function (id) {

        $http({
            url: "../stores/get_all_stores",
            method: "GET",
        }).then
        (function (mydata) {

            $scope.allStores = mydata.data.allStores;
        });
    }
    $scope.get_stores();
//===============================================

    $scope.get_stores_operaions= function () {

        $http({
            url: "../treasuries/treasuries_operations",
            method: "GET",
        }).then
        (function (mydata) {

            $scope.allOperations = mydata.data.allOperations;
        });
    }
    $scope.get_stores_operaions();
//================================================
   
}); //end controller


//=========================================

angular.module('myApp').filter('waredSum', function () {
    return function (list) {
        var sum = 0;
        for (var i = 0; i < list.length; i++) {

            sum += list[i].get_value;
        }
        return sum;
    }
});

angular.module('myApp').filter('monsarefSum', function () {
    return function (list) {
        var sum = 0;
        for (var i = 0; i < list.length; i++) {

            sum += list[i].give_value;
        }
        return sum;
    }
});

