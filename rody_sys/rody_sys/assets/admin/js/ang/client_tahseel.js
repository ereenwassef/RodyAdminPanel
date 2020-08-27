/// <reference path="../angular.js" />
/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);
app.controller("ctr_tahseel", function ($scope, $http) {
   

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
    //$scope.get_all_services = function () {
    //    $http({
    //        url: "../services/get_all_services",
    //        method: "GET",
    //    }).then
    //    (function (mydata) {
    //        $scope.allServices = mydata.data.allServices;
    //    });
    //}

    //$scope.get_all_services();
 //---------------------------------
    //$scope.get_all_sub_services = function () {
    //    $http({
    //        url: "../services/get_all_sub_services",
    //        method: "GET",
    //    }).then
    //    (function (mydata) {
    //        $scope.allServ = mydata.data.allServ;
    //    });
    //}
    //$scope.get_all_sub_services();
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
   
//=======================================================
    var cid = 0;

    $scope.get_sale_info = function (id,name) {

        cid = id;

        $scope.get_actDate();

       document.getElementById("cid").value = cid;
   
        $scope.cName = name;

        $http({
            url: "../sales/get_sale_info?cid="+id,
            method: "GET",
        }).then
        (function (mydata) {
            


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


            $scope.current = "الرصيد الحالي =";
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
                    "color": "white"
                }
             
            }


            $scope.des = "الرصيد المستحق =";
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
    $scope.get_actDate= function (id) {

        $http({
            url: "../clients/getActivationDateOfClient?id="+cid,
            method: "GET",
        }).then
        (function (mydata) {

            $scope.actDate = mydata.data.actDate;
        });
    }
//=======================================================
    
    $scope.get_stores = function (id) {

        $http({
            url: "../stores/get_all_stores",
            method: "GET",
        }).then
        (function (mydata) {

            $scope.allStores = mydata.data.allStores;
        });
    }

    $scope.get_stores();
//=======================================================

    $scope.get_store_value = function (id) {
        
        $http({
            url: "../stores/get_store_value?storeId=" + id,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.store_value_old = mydata.data.store_value;
        });
    }
//========================================================

    $scope.get_store_value2 = function (id) {

        $http({
            url: "../stores/get_store_value?storeId=" + id,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.currentValue3 = mydata.data.store_value;
        });
    }

//========================================================
    $scope.get_omolat = function () {

        $http({
            url: "../treasuries/get_my_parents?cid=" + cid + "&get_value=" + $scope.getValue,
            method: "GET",
        }).then
        (function (mydata) {

            $scope.total_omola = mydata.data.total_omola ;

            $scope.pList = [];
            var arr = new Array();

             arr = mydata.data.myParents;
            for (a in arr)
            {
                var arr2 = new Array();
                arr2 = arr[a].split("-");
               
                $scope.pList.push({id:arr2[0],name:arr2[1],value:arr2[2]});

            }
        });
    }
//======================================================
    
    var usType = 0;

    $scope.flip = function () {
        var type = document.getElementById("userType").value;

        var opType = document.getElementById("opType").value;

        if (opType == 1) //وارد
        {
            $scope.wared = true;
            $scope.monsaref = false;
            document.getElementById("btnOmolat").hidden = false;
        }
        else {
            $scope.wared = false;
            $scope.monsaref = true;
            document.getElementById("btnOmolat").hidden = true;
        }

        if (type == 1) {

            usType = 1;

            $scope.check1 = false;
            $scope.check2 = true;
            $scope.check3 = true;
        }
        else if (type == 2) {

            usType = 2;

            $scope.check2 = false;
            $scope.check1 = true;
            $scope.check3 = true;
        } else
        {
            usType = 3;

            $scope.check3 = false;
            $scope.check2 = true;
            $scope.check1 = true;
        }
    }
//=======================================================

    $scope.add_new_get = function () 
    {
        //alert(usType);
        var opType = document.getElementById("opType").value;

        if(usType==1)
        {
            if (opType == 1) //وارد
            {              
                $scope.add_new_get_client();
            }
            else
            {
               $scope.add_new_give_client();
            }
            
        }
        else  if(usType==3)
        {
            if (opType == 1) //وارد
            {
                $scope.add_new_get_store();
            }
            else
            {
                $scope.add_new_give_store();
            }
        }
    }
//=======================================================
    $scope.add_new_get_client = function () {

       var newValue= $scope.store_value_old + $scope.getValue;

        var storeId=document.getElementById("storeId").value;

        if (cid == 0 || $scope.getValue == null || storeId == "")
        {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        }
        else {

            document.getElementById("addBtn").disabled = true;

            $http({
                url: "../treasuries/wared_client",
                method: "POST",
                data: { cid: cid, storeId: storeId, getValue: $scope.getValue, storeValue: newValue, notes: $scope.notes, date: $scope.date },
            }).then
            (function (mydata) {
                alert(mydata.data.msg);

                //$scope.getValue = "";
                ////$scope.newValue = "";
                ////$scope.currentValue = "";
                //$scope.desValue = null;
                //$scope.cName = "";
                //$scope.actDate = "";
                //document.getElementById("storeId").value = "";
                //$scope.store_value_old = "";

                document.getElementById("storeId").value = "";
                $scope.store_value_old = "";
            });
        }

    }
//===============================================
    $scope.add_new_get_store = function () {

        var storeId1 = document.getElementById("storeId").value;
        var storeId2 = document.getElementById("storeId2").value;

        if (storeId2 == "" || $scope.getValue == null || storeId1 == "") {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        }
        else {

            document.getElementById("addBtn").disabled = true;

            $http({
                url: "../treasuries/wared_store",
                method: "POST",
                data: { storeId1: storeId1, storeId2: storeId2, getValue: $scope.getValue, notes: $scope.notes, date: $scope.date },
            }).then
            (function (mydata) {
                alert(mydata.data.msg);
                document.getElementById("storeId").value = "";
                $scope.store_value_old = "";
            });
        }

    }
//===============================================
    $scope.add_new_give_store = function () {

        var storeId1 = document.getElementById("storeId").value;
        var storeId2 = document.getElementById("storeId2").value;

        if (storeId2 == "" || $scope.getValue == null || storeId1 == "") {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        }
        else {

            document.getElementById("addBtn").disabled = true;

            $http({
                url: "../treasuries/monsaref_store",
                method: "POST",
                data: { storeId1: storeId1, storeId2: storeId2, getValue: $scope.getValue, notes: $scope.notes, date: $scope.date },
            }).then
            (function (mydata) {
                alert(mydata.data.msg);
                document.getElementById("storeId").value = "";
                $scope.store_value_old = "";
            });
        }

    }
//===============================================
    $scope.add_new_give_client = function () {

        var storeId = document.getElementById("storeId").value;

        if (cid == 0 || $scope.getValue == null || storeId == "") {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        }
        else {

            document.getElementById("addBtn").disabled = true;

            $http({
                url: "../treasuries/monsaref_client",
                method: "POST",
                data: { cid: cid, storeId: storeId, getValue: $scope.getValue, notes: $scope.notes, date: $scope.date },
            }).then
            (function (mydata) {
                alert(mydata.data.msg);

                //$scope.getValue = "";
                //$scope.newValue = "";
                //$scope.currentValue = "";
                //$scope.desValue = null;
                //$scope.cName = "";
                //$scope.actDate = "";
                //document.getElementById("storeId").value = "";
                //$scope.store_value_old = "";

                document.getElementById("storeId").value = "";
                $scope.store_value_old = "";

            });
        }

    }
//===============================================
    $scope.makeAddPossible = function () {
        document.getElementById("addBtn").disabled = false;
    }
//=======================================================
   









}); //end controller