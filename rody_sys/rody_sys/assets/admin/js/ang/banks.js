/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("banks", function ($scope, $http) {


    $scope.get_all_banks = function () {
        $http({
            url: "../banks/get_all_banks",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allBanks = mydata.data.allBanks;
        });
    }
    $scope.get_all_banks();
//==================================================

    var bankId = 0;

    $scope.get_bank = function (id) {
        
        bankId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../banks/delete_bank",
            method: "POST",
            data: { id: bankId },
        }).then
        (function (mydata) {
            $scope.get_all_banks();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {

        if ($scope.name == null) {

            alert("لم تتم عمليه الاضافه.. لا يمكن ان تدخل اسم البنك فارغ");
        }
        else if ((isNaN($scope.phone1) && $scope.phone1 != null) || (isNaN($scope.phone2) && $scope.phone2 != null) || (isNaN($scope.phone3) && $scope.phone3!=null))
        {
            alert("لم تتم عمليه الاضافه.. رقم التليفون غير صحيح");
        }
        else {
            $http({
                url: "../banks/add_bank",
                method: "POST",
                data: { name: $scope.name, address: $scope.address, phone1: $scope.phone1, phone2: $scope.phone2, phone3: $scope.phone3, fax: $scope.fax, responsible: $scope.responsible, notes: $scope.notes },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.address = "";
                $scope.phone1 = "";
                $scope.phone2 = "";
                $scope.phone3 = "";
                $scope.fax = "";
                $scope.responsible = "";
                $scope.notes = "";

                $scope.get_all_banks();
                alert(mydata.data.msg);
            });
        }
    }
//===============================================
  
    $scope.update = function (id, name, address, phone1, phone2, phone3, fax, responsible, notes)
    {

        bankId = id;
        $scope.name1 = name;
        $scope.address1 = address;
        $scope.phone11 = phone1;
        $scope.phone21 = phone2;
        $scope.phone31 = phone3;
        $scope.fax1 = fax;
        $scope.responsible1 = responsible;
        $scope.notes1 = notes;
    }

//===============================================
    $scope.updateDB = function () {

        if ($scope.name1 == null || $scope.name1=="") {

            alert("لم تتم عمليه التعديل.. لا يمكن ان تدخل اسم البنك فارغ");
        }
        else if ((isNaN($scope.phone11) && $scope.phone11 != null) || (isNaN($scope.phone21) && $scope.phone21 != null) || (isNaN($scope.phone31) && $scope.phone31 != null)) {
            alert("لم تتم عمليه التعديل.. رقم التليفون غير صحيح");
        }
        else {
            $http({
                url: "../banks/update_bank",
                method: "POST",
                data: {id:bankId, name: $scope.name1, address: $scope.address1, phone1: $scope.phone11, phone2: $scope.phone21, phone3: $scope.phone31, fax: $scope.fax1, responsible: $scope.responsible1, notes: $scope.notes1 },
            }).then
            (function (mydata) {
              
                $scope.get_all_banks();
                alert(mydata.data.msg);
            });
        }
    }
//================================================
   
}); //end controller