/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

//app.controller("agents", function ($scope, $http) {

angular.module('myApp').controller("ctr_agent", function ($scope, $http) {


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
//------------------------------
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

//--------------------------------------------------------------
    $scope.get_all=function()
    {
        $http({
            url: "../agents/get_all_agents",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allAgents = mydata.data.allAgents;
        });
    }

    $scope.get_all(); // to get all ...


//=======================================================
//-------------------------------------------------------

    var agentId = 0;

    $scope.get_agent = function (id) {

        agentId = id;
    }
//=================================================
    $scope.delete = function () {
        $http({
            url: "../agents/delete_agent",
            method: "POST",
            data: { id: agentId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {


        if ($scope.name == null || $scope.phone == null || $scope.address == null || $scope.govern2==null || $scope.area2==null) {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name) || isNaN($scope.phone)) {
            alert("لم تتم الاضافه .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../agents/add_agent",
                method: "POST",
                data: { name: $scope.name, phone: $scope.phone, address: $scope.address, areaId: $scope.area2 },
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
//===============================================

    $scope.update = function (id, name, phone,address,govern,area) {
        agentId = id;

        $scope.name2 = name;
        $scope.phone2 = phone;
        $scope.address2 = address;

        $scope.govern_update = govern;

        $scope.ar_update = area;

    }

//===============================================
    $scope.updateDB = function () {

        if ($scope.name2 == "" || $scope.phone2 == "" || $scope.address2 == "" || $scope.govern_update == "" || $scope.ar_update == "" || $scope.ar_update==null) {
            alert("لم تتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name2) || isNaN($scope.phone2)) {
            alert("لم تتم عمليه التعديل .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../agents/update_agent",
                method: "POST",
                data: { id: agentId, name: $scope.name2, phone: $scope.phone2, address: $scope.address2, areaId: $scope.ar_update },
            }).then
            (function (mydata) {
               
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
//===================================================

    $scope.icon_id = "fa-caret-up"; //default
    $scope.icon_name = "";
    $scope.icon_phone = "";
    $scope.icon_address = "";
    $scope.icon_area = "";
    $scope.icon_govern = "";

    $scope.sortedCol = "+id"; //default

    var previous_col = "id";
    var previous_icon = "up";

    $scope.sort = function (col) {
        previous_col = col;

        if (col == "id") {
            if (previous_icon == "up") {
                $scope.icon_id = "fa-caret-down";
                $scope.sortedCol = "-id";

                previous_icon = "down";

            }
            else {
                $scope.icon_id = "fa-caret-up";

                $scope.sortedCol = "+id";
                previous_icon = "up";
            }


            $scope.icon_name = "";
            $scope.icon_phone = "";
            $scope.icon_address = "";
            $scope.icon_area = "";
            $scope.icon_govern = "";
        }
        else if (col == "name")
        {
            if (previous_icon == "up") {
                $scope.icon_name = "fa-caret-down";
                $scope.sortedCol = "-name";

                previous_icon = "down";

            }
            else {
                $scope.icon_name = "fa-caret-up";

                $scope.sortedCol = "+name";
                previous_icon = "up";
            }


            $scope.icon_id = "";
            $scope.icon_phone = "";
            $scope.icon_address = "";
            $scope.icon_area = "";
            $scope.icon_govern = "";
        }
        else if (col == "phone") {
            if (previous_icon == "up") {
                $scope.icon_phone = "fa-caret-down";
                $scope.sortedCol = "-phone";

                previous_icon = "down";

            }
            else {
                $scope.icon_phone = "fa-caret-up";

                $scope.sortedCol = "+phone";
                previous_icon = "up";
            }


            $scope.icon_name = "";
            $scope.icon_id = "";
            $scope.icon_address = "";
            $scope.icon_area = "";
            $scope.icon_govern = "";
        }
        else if (col == "address") {
            if (previous_icon == "up") {
                $scope.icon_address = "fa-caret-down";
                $scope.sortedCol = "-address";

                previous_icon = "down";

            }
            else {
                $scope.icon_address = "fa-caret-up";

                $scope.sortedCol = "+address";
                previous_icon = "up";
            }


            $scope.icon_name = "";
            $scope.icon_phone = "";
            $scope.icon_id = "";
            $scope.icon_area = "";
            $scope.icon_govern = "";
        }
        else if (col == "areaName") {
            if (previous_icon == "up") {
                $scope.icon_area = "fa-caret-down";
                $scope.sortedCol = "-areaName";

                previous_icon = "down";

            }
            else {
                $scope.icon_area = "fa-caret-up";

                $scope.sortedCol = "+areaName";
                previous_icon = "up";
            }


            $scope.icon_name = "";
            $scope.icon_phone = "";
            $scope.icon_id = "";
            $scope.icon_address = "";
            $scope.icon_govern = "";
        }
        else if (col == "govern") {
            if (previous_icon == "up") {
                $scope.icon_govern = "fa-caret-down";
                $scope.sortedCol = "-govern";

                previous_icon = "down";

            }
            else {
                $scope.icon_govern = "fa-caret-up";

                $scope.sortedCol = "+govern";
                previous_icon = "up";
            }


            $scope.icon_name = "";
            $scope.icon_phone = "";
            $scope.icon_id = "";
            $scope.icon_address = "";
            $scope.icon_area = "";
        }  
        
    }
//==============================================



}); //end controller