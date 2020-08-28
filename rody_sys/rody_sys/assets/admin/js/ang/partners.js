/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);

app.controller("partners", function ($scope, $http) {


    $scope.get_all_partners = function () {
        $http({
            url: "../partners/get_all_partners",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allPartners = mydata.data.allPartners;
        });
    }
    $scope.get_all_partners();
//==================================================

    var pId = 0;

    $scope.get_partner = function (id) {
        
        pId = id;

    }
//=================================================
    $scope.delete = function () {

        $http({
            url: "../partners/delete_partner",
            method: "POST",
            data: { id: pId },
        }).then
        (function (mydata) {
            $scope.get_all_partners();
            alert(mydata.data.msg);
        });
    }
//================================================

    $scope.add = function () {


        //alert($scope.name + $scope.address + $scope.phone + $scope.mobile + $scope.money);


        if ($scope.name == null) {

            alert("لم يتم عمليه الاضافه.. لا يمكن ان تدخل قيمه الاسم فارغه");
        }
        else {
            $http({
                url: "../partners/add_partner",
                method: "POST",
                data: { name: $scope.name, address: $scope.address, phone: $scope.phone, mobile: $scope.mobile, money: $scope.money },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.address = "";
                $scope.phone = "";
                $scope.mobile = "";
                $scope.money = "";

                $scope.get_all_partners();

                alert(mydata.data.msg);
            });
        }
    }
//===============================================
       

    $scope.update = function (id, name, address, phone, mobile, money)
    {

        pId = id;

        $scope.name2 = name;
        $scope.address2 = address;
        $scope.phone2 = phone;
        $scope.mobile2 = mobile;
        $scope.money2 = money;
        
    }

//===============================================
    $scope.updateDB = function () {
        
        if ($scope.name2 == null) {
            alert("لم يتم العديل  .. لا يمكن ان تدخل قيمه الاسم فارغه");
        } 
        else {

            $http({
                url: "../partners/update_partner",
                method: "POST",
                data: { id: pId, name: $scope.name2, address: $scope.address2, phone: $scope.phone2, mobile: $scope.mobile2, money: $scope.money2 },
            }).then
            (function (mydata) {
                $scope.get_all_partners();
                alert(mydata.data.msg);
            });
        }
    }

//================================================
   
}); //end controller