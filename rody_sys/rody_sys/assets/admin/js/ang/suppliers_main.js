/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("suppliers_main", function ($scope, $http) {

    
    $scope.get_all_main=function()
    {
        $http({
            url: "../suppliers/get_all_main_categories",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.all_main_category = mydata.data.mainCategories;
        });
    }

    $scope.get_all_main(); // to get all ...
//==================================================

    var mainId = 0;

    $scope.get_main = function (id) {
        
        mainId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../suppliers/delete_main_category",
            method: "POST",
            data: { id: mainId },
        }).then
        (function (mydata) {
            $scope.get_all_main();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/add_main_category",
                method: "POST",
                data: { name: $scope.name },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.get_all_main();
                alert(mydata.data.msg);
            });
        }
    }
//===============================================
       

    $scope.update=function(id,name)
    {
        mainId = id;
        $scope.name2 = name;
    }

//===============================================
  
    $scope.updateDB = function () {

        if ($scope.name2 == null) {

            alert("لم يتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/update_main_category",
                method: "POST",
                data: {id:mainId, name: $scope.name2 },
            }).then
            (function (mydata) {
                $scope.get_all_main();
                alert(mydata.data.msg);
            });
        }
    }

//================================================
   
}); //end controller