/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);
app.controller("ctr_sale2", function ($scope, $http) {

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
            url: "../sales/get_all_sells",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allSells = mydata.data.allSells;
        });
    }

    $scope.get_all(); // to get all ...
//---------------------------------------------------------------
    var saleId = 0;
    $scope.get_sale=function(id)
    {
        saleId = id;
    }
//-------------------------------------------------------------
    $scope.getClient = function (id,name) {
        $scope.client_s_id = id;
        $scope.client_s_name = name;
    }
//---------------------------------------------------------------

    $scope.deleteSaleOperation = function () {
        $http({
            url: "../sales/delete_sale_operation",
            method: "POST",
            data: { saleId: saleId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//----------------------------------------------------------------
    
    $scope.get_all_clients = function () {
        $http({
            url: "../clients/get_all_clients",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
        });
    }

    $scope.get_all_clients(); // to get all ...

//----------------------------------------------------------
    $scope.get_all_services = function () {

        $http({
            url: "../services/get_all_services",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allServices = mydata.data.allServices;
        });
    }

    $scope.get_all_services(); // to get all ...
//-----------------------------------------------------------

    $scope.get_all_getPhoneNumHistories = function () {
        $http({
            url: "../sales/getPhoneNumHistories",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.phoneHistory = mydata.data.phoneHistory;
        });
    }
    $scope.get_all_getPhoneNumHistories();

//====================================================================


}); //end controller


function get_js_date(date) {

    var dateTokens = date.split("/");

    var day = parseInt(dateTokens[0]);
    var month = parseInt(dateTokens[1]) - 1;
    var year = parseInt(dateTokens[2]);

    var jsDate = new Date(year, month, day);

    return jsDate;
}
//=========================================================
function get_js_date_from_to(date) {

    var dateTokens = date.split("-");

    var day = parseInt(dateTokens[2]);
    var month = parseInt(dateTokens[1]) - 1;
    var year = parseInt(dateTokens[0]);

    var jsDate = new Date(year, month, day);

    return jsDate;
}
//=======================================================
angular.module('myApp').filter('myFilter2', function () {
    return function (list, from, to) {

          //alert("999");

        if (from || to) {

            var result = [];

            for (var i = 0; i < list.length; i++) {

                var date = get_js_date(list[i].actual_date);

                if (from == null || from == "" && to != null) {
                    if (date <= get_js_date_from_to(to)) {

                        result.push(list[i]);
                    }
                }
                else if (to == null || to == "" && from != null) {
                    if (date >= get_js_date_from_to(from)) {

                        result.push(list[i]);
                    }
                }
                else {
                    if (date >= get_js_date_from_to(from) && date <= get_js_date_from_to(to)) {

                        result.push(list[i]);
                    }
                }
            }

            return result;
        }
        else {
            return list;
        }
    };
});

//=========================================

angular.module('myApp').filter('sumFilterDeptor', function () {
    return function (list) {
        var sum = 0;
        for (var i = 0; i < list.length; i++) {

            sum += list[i].debtor;
        }
        return sum;
    }
});