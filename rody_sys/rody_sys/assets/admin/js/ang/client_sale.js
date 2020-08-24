/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("ctr_sale", function ($scope, $http) {

   // document.getElementById("storeOld").value = 120;

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

    //------------------------------
    $scope.get_all_services = function () {
        $http({
            url: "../services/get_all_services",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allServices = mydata.data.allServices;
        });
    }

    $scope.get_all_services();
    //---------------------------------------
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
//=======================================================
    $scope.get_all=function()
    {
        $http({
            url: "../clients/get_all_clients",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
        });
    }

    $scope.get_all(); // to get all ...

//=======================================================
    var shId = 0;

    document.getElementById("services").onchange = function () {
        $http({
            url: "../stores/get_shareha_charge?serviceId=" + document.getElementById("services").value,
            method: "GET",
        }).then
        (function (mydata) {

            $scope.storeOld = mydata.data.shareha_charge;
            $scope.price = mydata.data.price;
            shId = mydata.data.shId;
        });
    }

//========================================================


    var cid = 0;

    $scope.get_sale_info = function (id, name) {


        cid = id;

       // document.getElementById("cid").value = cid;
   
        $scope.cName = name;

        $http({
            url: "../sales/get_sale_info?cid=" + id +"&shId="+shId,
            method: "GET",
        }).then
        (function (mydata) {
            

            $scope.phones = mydata.data.phones;

            $scope.currentValue = "";
            $scope.currentType = "";
            $scope.desValue = "";
            $scope.desType = "";
            $scope.myStyle = {
                "background-color": "none",
            };
            $scope.myStyle2 = {
                "background-color": "none",
            };


            $scope.current = "الرصيد الحالي";
            $scope.currentValue = mydata.data.current;
            if(mydata.data.current<0)
            {
                $scope.currentType = "دائن";
                $scope.myStyle = {
                    "background-color": "#7ac29a",
                    "color": "white"
                }
              
            }
            else if (mydata.data.current > 0)
            {
                $scope.currentType= "مدين";
                $scope.myStyle = {
                    "background-color": "#d68181",
                    "color":"white"
                }
             
            }


            $scope.des = "الرصيد المستحق";
            $scope.desValue = mydata.data.deserved;
            if (mydata.data.deserved < 0) {

                $scope.desType = "دائن";
                $scope.myStyle2 = {
                    "background-color": "#7ac29a",
                    "color": "white"
                }

            }
            else if (mydata.data.deserved > 0) {
                $scope.desType = "مدين";
                $scope.myStyle2 = {
                    "background-color": "#d68181",
                    "color": "white"
                }

            }


        });
    }
//=======================================================
    $scope.addSaleOperation = function () {

        if (cid == 0 || document.getElementById("services").value == "" || (document.getElementById("phoneId").value == "" && $scope.phoneNum==null) || $scope.charge == null)
        {
            alert("لا يمكن ان تدخل قيم فارغه .. حاول مره اخري")
        }
         else{
            document.getElementById("addBtn").disabled = true;
            $http({
                url: "../sales/add_new_sale",
                method: "POST",
                data: { cid: cid, serviceId: document.getElementById("services").value, phoneId: document.getElementById("phoneId").value, chargeValue: $scope.charge, total: document.getElementById("total").value, notes: $scope.notes, time: $scope.time, date: $scope.date, otherPhoneNum: $scope.phoneNum },
            }).then
               (function (mydata) {
                   alert(mydata.data.msg);
               });
    }

    }
//======================================================
    $scope.makeAddPossible = function () {
        document.getElementById("addBtn").disabled = false;
    }
//=======================================================
  

}); //end controller