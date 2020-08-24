/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("suppliers_sub", function ($scope, $http) {

    
    $scope.get_all_sub=function()
    {
        $http({
            url: "../suppliers/get_all_sub_categories",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.all_sub_category = mydata.data.subCategories;
        });
    }

    $scope.get_all_sub(); // to get all ...
//================================================

    $scope.get_all_main = function () {
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

    var subId = 0;

    $scope.get_sub = function (id,name,mainId) {
        
        subId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../suppliers/delete_sub_category",
            method: "POST",
            data: { id: subId },
        }).then
        (function (mydata) {
            $scope.get_all_sub();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null || $scope.main==null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/add_sub_category",
                method: "POST",
                data: { name: $scope.name , mainId:$scope.main},
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.get_all_sub();
                alert(mydata.data.msg);
            });
        }
    }

//===============================================
       

    $scope.update=function(id,name,mainId)
    {
        subId = id;
        $scope.name2 = name;
        $scope.main2 = mainId;
    }

//===============================================

    $scope.updateDB = function () {

        if ($scope.name2 == null || $scope.main2 == null) {

            alert("لم يتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/update_sub_category",
                method: "POST",
                data: {id:subId, name: $scope.name2, mainId: $scope.main2 },
            }).then
            (function (mydata) {
                $scope.get_all_sub();
                alert(mydata.data.msg);
            });
        }
    }


//================================================
   
}); //end controller