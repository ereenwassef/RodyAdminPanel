/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("ctr_clientH", function ($scope, $http) {

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
    $scope.clear = function ()
    {
        $scope.TS = "";
        $scope.badal_tahseel = "";
        $scope.tahseel = "";
        $scope.amola = "";
        $scope.actual_charge = "";
        $scope.deserved = "";
    }
//=======================================================
    $scope.get_my_total = function (id) {

        $scope.clear();

        $http({
            url: "../clients/get_total_client_operations?cid=" + id + "&startDate=" + $scope.from + "&endDate=" + $scope.to,
            method: "GET",
        }).then
        (function (mydata) {

            $scope.TS = mydata.data.sales+" جنيه ";
            $scope.badal_tahseel = mydata.data.badal_tahseel + " جنيه ";
            $scope.tahseel = mydata.data.tahseel + " جنيه ";
            $scope.amola = mydata.data.amola + " جنيه ";
            $scope.actual_charge = mydata.data.actual_charge + " جنيه ";
            $scope.deserved = mydata.data.deserved + " جنيه ";

        });

    }
//=======================================================

    var cltId = 0;

    $scope.choose = function (cid, name) {

        cltId = cid;

        if (name != "") {
            document.getElementById("clientName").value = name;
        }

        $http({
            url: "../clients/get_all_client_operations?id=" + cid,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allOperations = mydata.data.allOperations;

            $scope.get_my_total(cid);

            $scope.update_client(mydata.data.clinetInfo);

        });
    }
//================================================
    $scope.getTotal= function (cid, name)
    {
        $scope.get_my_total(cltId);
    }
//================================================

    $scope.init = function () {
       
       var id=document.getElementById("cltId").value;

        $scope.choose(id,"");

    };
    $scope.init();
//================================================
    $scope.add_mostahek = function () {

        if (cltId == 0) {
            alert("اختر العميل اولا!!")
        }
        else {
            $http({
                url: "../reports/add_permissions",
                method: "POST",
                data: { cltId: cltId },
            }).then
            (function (mydata) {
                alert(mydata.data.msg);
            });
        }
    }
//================================================

    var clientId = 0;

    $scope.update_client = function (c) {

        clientId = c.id;

        $scope.get_sale_info(c.id);

        $scope.get_client_phones();

        $scope.name_2 = c.name;
        $scope.contractDate_2 = c.contract_date;
        $scope.actDate = c.activation_date;
        $scope.areaS_2 = c.areaId;
        $scope.getPlace_2 = c.stay_place_get;
        parentId = c.parentId;//client parent
        $scope.pName_2 = c.pName;
        $scope.jop_2 = c.jop;
        $scope.DOB_2 = c.date_of_birth;
        $scope.idNum_2 = c.id_num;
        $scope.governS_2 = c.governId;
        $scope.idPlace_2 = c.id_place;
        $scope.workName_2 = c.work_name;
        $scope.workPlace_2 = c.work_place;
        $scope.stayPlace_2 = c.stay_place;
        $scope.floor_2 = c.stay_place_floor;
        $scope.flat_2 = c.stay_place_flat;
        $scope.deleS_2 = c.delegatorId;
        $scope.agentsS_2 = c.agentId;
        $scope.available = c.available;
        $scope.notes_2 = c.notes;
        $scope.chargeMsg_2 = c.MsgCharge;

    }
//======================================================
    $scope.updateDB = function () {

        if ($scope.name_2 == null) {
            alert("لم تتم عمليه التعديل .. لا يمكن ان تترك قيمه اسم العميل فارغه ");
        }
        else {

            $http({
                url: "../clients/update_client",
                method: "POST",
                data: {
                    cId: clientId,
                    jop: $scope.jop_2,
                    idNum: $scope.idNum_2,
                    name: $scope.name_2,
                    parentId: parentId,
                    phone1: $scope.ph1_2,
                    shreha1: $scope.sh1_2,
                    phone2: $scope.ph2_2,
                    shreha2: $scope.sh2_2,
                    phone3: $scope.ph3_2,
                    shreha3: $scope.sh3_2,
                    areaId: $scope.areaS_2,
                    getPlace: $scope.getPlace_2,
                    workPlace: $scope.workPlace_2,
                    workName: $scope.workName_2,
                    flat: $scope.flat_2,
                    floor: $scope.floor_2,
                    stayPlace: $scope.stayPlace_2,
                    delegatorId: $scope.deleS_2,
                    idPlace: $scope.idPlace_2,
                    notes: $scope.notes_2,
                    chargeMsg: $scope.chargeMsg_2,
                    available: $scope.available,
                }
            }).then
            (function (mydata) {
                $scope.get_all();
                alert(mydata.data.msg);
            },
            function (errorReason) {
                alert("an error occurs" + errorReason.data);
            });

        }
    }
//======================================================

    $scope.get_client_phones = function () {

        $http({
            url: "../clients/get_client_phones?cId=" + clientId,
            method: "GET",
        }).then
        (function (mydata) {
            // $scope.allClients = mydata.data.allClients;

            if (mydata.data.phones[0] != null) {
                $scope.ph1_2 = mydata.data.phones[0].number;
                $scope.sh1_2 = mydata.data.phones[0].sharehaId;
            }
            else {
                $scope.ph1_2 = "";
            }

            if (mydata.data.phones[1] != null) {
                $scope.ph2_2 = mydata.data.phones[1].number;
                $scope.sh2_2 = mydata.data.phones[1].sharehaId;
            }
            else {
                $scope.ph2_2 = "";
            }

            if (mydata.data.phones[2] != null) {
                $scope.ph3_2 = mydata.data.phones[2].number;
                $scope.sh3_2 = mydata.data.phones[2].sharehaId;
            }
            else {
                $scope.ph3_2 = "";
            }

        });
    }
//======================================================
    $scope.get_sale_info = function (id) {

        $http({
            url: "../sales/get_sale_info?cid=" + id,
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


            $scope.current = "الرصيد الحالي";
            $scope.currentValue = mydata.data.current;
            if (mydata.data.current < 0) {
                $scope.currentType = "دائن";
                $scope.myStyle = {
                    "background-color": "#7ac29a",
                    "color": "white"
                }

            }
            else if (mydata.data.current > 0) {
                $scope.currentType = "مدين";
                $scope.myStyle = {
                    "background-color": "#d68181",
                    "color": "white"
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
    $scope.get_all_shareeha = function () {
        $http({
            url: "../stores/get_all_total_charge",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.kroot = mydata.data.totalCharge;
            $scope.sh1 = mydata.data.totalCharge[0].id;
            $scope.sh2 = mydata.data.totalCharge[0].id;
            $scope.sh3 = mydata.data.totalCharge[0].id;

            $scope.sh1_2 = mydata.data.totalCharge[0].id;
            $scope.sh2_2 = mydata.data.totalCharge[0].id;
            $scope.sh3_2 = mydata.data.totalCharge[0].id;
        });
    }
    $scope.get_all_shareeha();
//=========================================================


}); //end controller
//==============================================================

angular.module('myApp').filter('myActualDateFilter_in', function () {
    return function (list, from, to) {

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
