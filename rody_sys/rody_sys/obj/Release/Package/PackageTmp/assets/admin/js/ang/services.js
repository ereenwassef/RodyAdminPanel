/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("services", function ($scope, $http) {


    $scope.get_all=function()
    {

        $http({
            url: "../services/get_all_services",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allServices = mydata.data.allServices;
        });
    }

    $scope.get_all(); // to get all ...
//====================================================

    $scope.get_all_shareha = function () {

        $http({
            url: "../stores/get_all_total_charge",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.totalCharge = mydata.data.totalCharge;
        });
    }

    $scope.get_all_shareha(); // to get all ...
//==================================================
    var servId = 0;

    $scope.get_service = function (id) {
               
        servId = id;

    }
//===================================================
    $scope.delete = function () {

        $http({
            url: "../services/delete_service",
            method: "POST",
            data: { id: servId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//================================================
    $scope.add = function () {

        if ($scope.name == null || $scope.price == null || $scope.sharehaId == null) {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        } 
        else {

            $http({
                url: "../services/add_service",
                method: "POST",
                data: { name: $scope.name, sellPrice: $scope.price, sharehaId: $scope.sharehaId },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.price = "";
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
//=======================================================
    $scope.update = function (id,name,shareeha,price)
    {
        servId = id;

        $scope.name2 = name;
        $scope.price2 = price;
        document.getElementById("sharehaId2").value = shareeha;
    }
//=======================================================
    $scope.updateDB = function () {

        if ($scope.name2 == null || $scope.price2 == "" || document.getElementById("sharehaId2").value == null) {
            alert("لم تتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        }
        else {

            $http({
                url: "../services/update_services",
                method: "POST",
                data: { id: servId, name: $scope.name2, sellPrice: $scope.price2, sharehaId: document.getElementById("sharehaId2").value },
            }).then
            (function (mydata) {
              
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
    //=======================================================
    function fun() {
        $(document).ready(function () {

            var ctx = $("#mycanvas").get(0).getContext("2d");
              
            var data = [

                {
                    value: parseInt(document.getElementById("value1").value),
                    color: "#97efbe",
                    // highlight: "yellow",
                    label: document.getElementById("serv1").value + ":" + parseInt(document.getElementById("value1").value)
                },
                   {
                       value: parseInt(document.getElementById("value2").value),
                       color: "pink",
                       // highlight: "yellow",
                       label: document.getElementById("serv2").value + ":" + document.getElementById("value2").value
                   },
                      {
                          value: parseInt(document.getElementById("value3").value),
                          color: "yellow",
                          // highlight: "yellow",
                          label: document.getElementById("serv3").value + ":" + document.getElementById("value3").value
                      },
                         //{
                         //    value: parseInt(document.getElementById("value4").value),
                         //    color: "green",
                         //    // highlight: "yellow",
                         //    label: document.getElementById("serv4").value + ":" + document.getElementById("value4").value
                         //},
                         //   {
                         //       value: parseInt(document.getElementById("value5").value),
                         //       color: "black",
                         //       // highlight: "yellow",
                         //       label: document.getElementById("serv5").value + ":" + document.getElementById("value5").value
                         //   },
            ];

            //draw
            var piechart = new Chart(ctx).PolarArea(data);
        });
    }
   


//=======================================================
    $scope.get_chart_value= function (id) {
        try{
            document.getElementById("serv1").value = "";
            document.getElementById("value1").value = 0
            document.getElementById("serv2").value = "";
            document.getElementById("value2").value = 0;
            document.getElementById("serv3").value = "";
            document.getElementById("value3").value = 0;
            document.getElementById("serv4").value = "";
            document.getElementById("value4").value = 0;
            document.getElementById("serv5").value = "";
            document.getElementById("value5").value = 0;

            $http({
                url: "../services/servicesChart_get_top_5?"+"startDate=" + $scope.from + "&endDate=" + $scope.to,
                method: "GET",
            }).then
            (function (mydata) {

                for (var i = 0; i < mydata.data.top5.length; i++)
                {
                    var s = 'serv' + parseInt(i + 1);
                    var v = 'value' + parseInt(i + 1);

                    document.getElementById(s).value = mydata.data.top5[i].name;
                    document.getElementById(v).value = mydata.data.top5[i].count;
                }

                fun();
            
            });
        }catch (e)
        {

        }

    }

    $scope.get_chart_value();

 
//==========================================================




}); //end controller