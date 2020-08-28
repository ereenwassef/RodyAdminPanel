/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);
app.controller("suppliers", function ($scope, $http) {

    
    $scope.get_all=function()
    {

        $http({
            url: "../suppliers/get_all_suppliers",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allSuppliers = mydata.data.allSuppliers;
        });
    }

    $scope.get_all(); // to get all ...
//==================================================
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
//===================================================
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
//===================================================
    var subId = 0;

    $scope.choose = function (id, name, charge) {
        
        subId = id;
        $scope.supName = name;

        $scope.get_sub_history();
    }

//===================================================
    $scope.get_sub_history = function () {
        $http({
            url: "../suppliers/get_supplier_history?id="+subId,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.supplierHistory = mydata.data.supplierHistory;
        });
    }

    $scope.get_sub_history(); // to get all ...
//===================================================
    var sId = 0;

    $scope.get_supplier = function (id) {
        
        sId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../suppliers/delete_supplier",
            method: "POST",
            data: { id: sId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null || document.getElementById("tasneefId").value == "" || document.getElementById("type").value == "") {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/add_supplier",
                method: "POST",
                data: { name: $scope.name, address: $scope.address, responsible: $scope.responsible, phone1: $scope.phone1, phone2: $scope.phone2, phone3: $scope.phone3, fax: $scope.fax, tasneefId: document.getElementById("tasneefId").value, type: $scope.type, notes: $scope.notes },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.address = "";
                $scope.responsible = "";
                $scope.phone1 = "";
                $scope.phone2 = "";
                $scope.phone3 = "";
                $scope.fax = "";
                $scope.notes = "";

                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
//===============================================
       

    $scope.update = function (id, name, address, responsible, phone1, phone2, phone3, fax, tasneefId, type, notes , charge)
    {
        sId = id;
        $scope.name2 = name;
        $scope.address2 = address;
        $scope.responsible2 = responsible;
        $scope.phone12 = phone1;
        $scope.phone22 = phone2;
        $scope.phone32 = phone3;
        $scope.fax2 = fax;
        document.getElementById("tasneefId2").value = tasneefId;
        document.getElementById("type2").value = type;
        $scope.notes2 = notes;
        $scope.charge = charge;
    }

//===============================================
    $scope.updateDB = function () {


        if ($scope.name2 == null || document.getElementById("tasneefId2").value == "" || document.getElementById("type2").value == "") {

            alert("لم يتم عمليه التعديل .. لا يمكن ان تدخل قيم فارغه");
        }
        else {
            $http({
                url: "../suppliers/update_supplier",
                method: "POST",
                data: { id: sId, name: $scope.name2, address: $scope.address2, responsible: $scope.responsible2, phone1: $scope.phone12, phone2: $scope.phone22, phone3: $scope.phone32, fax: $scope.fax2, tasneefId: document.getElementById("tasneefId2").value, type: document.getElementById("type2").value, notes: $scope.notes2 },
            }).then
            (function (mydata) {
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
//================================================
   
}); //end controller














