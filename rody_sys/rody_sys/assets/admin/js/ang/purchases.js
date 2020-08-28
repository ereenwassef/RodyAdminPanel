/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);

app.controller("purchases", function ($scope, $http) {

    $scope.get_all_allPurchases = function () {
        $http({
            url: "../purchases/get_all_purchases",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allPurchases = mydata.data.allPurchases;
        });
    }
    $scope.get_all_allPurchases();
//=================================================
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


    $scope.chooseSup = function (id, name) {
        $scope.sId = id;
        $scope.supName_2 = name;
    }



    //var tId = 0;

    //$scope.get_treasury = function (id) {
        
    //    tId = id;
    //}
//=================================================
    //$scope.delete = function () {

    //    $http({
    //        url: "../treasuries/delete_Treasury",
    //        method: "POST",
    //        data: { id: tId },
    //    }).then
    //    (function (mydata) {
    //        $scope.get_all_treasuries();
    //        alert(mydata.data.msg);
    //    });
    //}
//================================================

    //$scope.add = function () {

    //    if ($scope.name == null || $scope.value==null) {

    //        alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
    //    }
    //    else {
    //        $http({
    //            url: "../treasuries/add_Treasury",
    //            method: "POST",
    //            data: { name: $scope.name, value: $scope.value },
    //        }).then
    //        (function (mydata) {
    //            $scope.name = "";
    //            $scope.value=""
    //            $scope.get_all_treasuries();
    //            alert(mydata.data.msg);
    //        });
    //    }
    //}
//===============================================
       

    //$scope.update=function(id,name,value)
    //{
    //    tId = id;
    //    $scope.name2 = name;
    //    $scope.value2 = value;
    //}

//===============================================
    //$scope.updateDB = function () {

    //    if ($scope.name2 == null || $scope.value2 == null) {

    //        alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
    //    }
    //    else {
    //        $http({
    //            url: "../treasuries/update_Treasury",
    //            method: "POST",
    //            data: { id:tId,name: $scope.name2, value: $scope.value2 },
    //        }).then
    //        (function (mydata) {
              
    //            $scope.get_all_treasuries();
    //            alert(mydata.data.msg);
    //        });
    //    }
    //}
//================================================

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
//====================================================
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


//====================================================
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

//================================================
    var cId = 0;
    $scope.choose2 = function (cid, name , charge) {
        cId = cid;
        $scope.cName = name;
        $scope.client_charge_before = charge;
        
    }
//================================================
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
//================================================
    $scope.get_all_sub = function () {
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
    $scope.get_all = function () {
        $http({
            url: "../suppliers/get_all_suppliers",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allSuppliers = mydata.data.allSuppliers;
        });
    }

    $scope.get_all(); // to get all ...
//================================================
    
    $scope.flip = function ()
    {

        if (document.getElementById("sellerType").value == 1)
        {
            $scope.checked2 = true;
            $scope.checked1 = false;

        }else
        {
            $scope.checked1 = true;
            $scope.checked2 = false;
        }

        if (document.getElementById("businessType").value == 1)
        {
            document.getElementById("treasuryId").disabled = true;
            document.getElementById("paidValue").disabled = true;
        }
        else
        {
            document.getElementById("treasuryId").disabled = false;
            document.getElementById("paidValue").disabled = false;
        }    

    }
//================================================

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

//================================================
    var subId = 0;

    $scope.choose = function (id, name, charge)
    {
        subId = id;
        $scope.supName = name;
        $scope.sup_charge_before = charge;
    }
//================================================
    //var tValue = 0;
    //$scope.get_treasury_value = function (tId) {
    //    alert("0");
    //    $http({
    //        url: "../treasuries/get_treasury_value?tId="+tId,
    //        method: "GET",
    //    }).then
    //    (function (mydata) {
    //        alert("1");
    //        tValue = mydata.data.value;
    //    },
    //    function(errorReason)
    //    {
    //        alert("an error occurs" + errorReason.data);
    //    });

    //    return tValue;
    //}

    //alert("vv=" + tValue);
   // $scope.get_treasury_value(document.getElementById("treasuryId").value);
//================================================
    $scope.add = function () {

        if ($scope.price == null || $scope.quantity == null || (subId==0 && cId==0)) {
            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            document.getElementById("addBtn").disabled = true;
            $http({
                url: "../purchases/add_purchase",
                method: "POST",

                data: { date: $scope.date, supplierId: subId,clientId:cId ,shareehaId: document.getElementById("sharehaId").value, quantity: $scope.quantity, price: $scope.price, discount: $scope.discount, storeId: document.getElementById("treasuryId").value, payValue: $scope.paidValue, sellerType: document.getElementById("sellerType").value, businessType: document.getElementById("businessType").value },

            }).then
            (function (mydata) {
                alert(mydata.data.msg);
            });
        }
    }
//=================================================================
    $scope.makeAddPossible=function()
    {
        document.getElementById("addBtn").disabled = false;
    }
//=================================================================
    $scope.get_omolat = function () {

        $http({
            url: "../treasuries/get_my_parents?cid=" + cId + "&get_value=" +(( $scope.quantity*$scope.price)-$scope.discount),
            method: "GET",
        }).then
        (function (mydata) {


            $scope.total_omola = mydata.data.total_omola;

            $scope.pList = [];
            var arr = new Array();

            arr = mydata.data.myParents;
            for (a in arr) {
                var arr2 = new Array();
                arr2 = arr[a].split("-");

                $scope.pList.push({ id: arr2[0], name: arr2[1], value: arr2[2] });

            }



        });
    }

    //=======================================================

 //================================================

    $scope.main=function()
    {
        $scope.discount = 0;
    }

    $scope.main();
   
}); //end controller