/// <reference path="../angular.js" />
/// <reference path="../../../general/js/angular.js" />


var app = angular.module("myApp", []);
app.controller("c_types", function ($scope, $http) {

    
    $scope.get_all=function()
    {
        $http({
            url: "../clients/get_all_types",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allTypes = mydata.data.allTypes;
        });
    }

    $scope.get_all(); // to get all ...
//==================================================

    var typeId = 0;

    $scope.get_type = function (id) {
        
        typeId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../clients/delete_type",
            method: "POST",
            data: { id: typeId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null || $scope.name=="") {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../clients/add_type",
                method: "POST",
                data: { type: $scope.name },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
    //===============================================
       

    $scope.update=function(id,name)
    {
        typeId = id;
        $scope.name2 = name;
    }

    //===============================================
    $scope.updateDB = function () {
        
        if ($scope.name2 == null || $scope.name2=="") {
            alert("لم يتم العديل  .. لا يمكن ان تدخل قيم فارغه");
        } 
        else {

            $http({
                url: "../clients/update_type",
                method: "POST",
                data: {id:typeId, type: $scope.name2},
            }).then
            (function (mydata) {
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }

//================================================
   

//=======================================================
}); //end controller