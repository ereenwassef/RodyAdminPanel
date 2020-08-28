/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);

app.controller("stores", function ($scope, $http) {


    $scope.get_all_shareeha = function () {
        $http({
            url: "../stores/get_all_total_charge",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.kroot = mydata.data.totalCharge;
        });
    }
    $scope.get_all_shareeha();
//==================================================
    var tId = 0;

    $scope.get_shareeha = function (id) {
        
        tId = id;
    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../stores/delete_shareeha",
            method: "POST",
            data: { id: tId },
        }).then
        (function (mydata) {
            $scope.get_all_shareeha();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null || $scope.value==null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../stores/add_shareeha",
                method: "POST",
                data: { name: $scope.name, value: $scope.value },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.value=""
                $scope.get_all_shareeha();
                alert(mydata.data.msg);
            });
        }
    }
//===============================================
       

    $scope.update=function(id,name,value)
    {
        tId = id;
        $scope.name2 = name;
        $scope.value2 = value;
    }

//===============================================
    $scope.updateDB = function () {

        if ($scope.name2 == null || $scope.value2 == null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../stores/update_shareeha",
                method: "POST",
                data: { id:tId,name: $scope.name2, value: $scope.value2 },
            }).then
            (function (mydata) {
              
                $scope.get_all_shareeha();
                alert(mydata.data.msg);
            });
        }
    }

//================================================
   
}); //end controller