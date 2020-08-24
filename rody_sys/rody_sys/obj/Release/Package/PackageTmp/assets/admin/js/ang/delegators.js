/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("ctr_dele", function ($scope, $http) {


    $scope.get_all=function()
    {
        $http({
            url: "../delegators/get_all_delegators",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allDelegators = mydata.data.allDelegators;
        });
    }

    $scope.get_all(); // to get all ...
//------------------------------------------------
    $scope.get_all_governs = function () {
        $http({
            url: "../governs/get_all_govern",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allGovern = mydata.data.allGovern;
            $scope.allGovern2 = mydata.data.allGovern;
        });
    }
    $scope.get_all_governs();
//------------------------------------------------
    $scope.get_all_areas = function () {
        $http({
            url: "../areas/get_all_areas",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allArea = mydata.data.allArea;
            $scope.allArea2 = mydata.data.allArea;
        });
    }

    $scope.get_all_areas();

//================================================
    var deleId = 0;

    $scope.get_delegator = function (id) {
        deleId = id;
    }
//===================================================
    $scope.delete = function () {
        $http({
            url: "../delegators/delete",
            method: "POST",
            data: { id: deleId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.get_all_agents = function () {
        $http({
            url: "../agents/get_all_agents",
            method: "GET",
        }).then
        (function (mydata) {          
            $scope.allAgents = mydata.data.allAgents;
        });
    }
    $scope.get_all_agents();

//====================================================
    $scope.add = function () {

        if ($scope.name == null || $scope.phone == null || $scope.address == null || $scope.agentId == null) {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name) || isNaN($scope.phone)) {
            alert("لم تتم الاضافه .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../delegators/add_delegator",
                method: "POST",
                data: { name: $scope.name, phone: $scope.phone, address: $scope.address, agentId: $scope.agentId },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.phone = "";
                $scope.address = "";
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
//=======================================================
    $scope.update = function (id,name,phone,address,agentId,active)
    {

        deleId = id;

        $scope.name2 = name;
        $scope.phone2 = phone;
        $scope.address2 = address;
        $scope.agentId2 = agentId;

        document.getElementById("active").value = active;
    }
//=======================================================
    $scope.updateDB = function () {

        if ($scope.name2 == null || $scope.phone2 == null || $scope.address2 == null || $scope.agentId2 == null) {
            alert("لم تتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name2) || isNaN($scope.phone2)) {
            alert("لم تتم عمليه التعديل .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../delegators/update_delegator",
                method: "POST",
                data: { id: deleId, name: $scope.name2, phone: $scope.phone2, address: $scope.address2, agentId: $scope.agentId2, active: document.getElementById("active").value },
            }).then
            (function (mydata) {
           
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }

//=======================================================
}); //end controller