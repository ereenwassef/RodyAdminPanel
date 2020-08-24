/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

var charge_first;
var sales;
var tahseel;
var badal;
var omolat;
var charge_last;

var charge_first2;
var sales2;
var tahseel2;
var badal2;
var omolat2;
var charge_last2;

angular.module('myApp').controller("ctr_client", function ($scope, $http) {

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
//-----------------------------------------------------
    $scope.get_all_areas = function () {
        $http({
            url: "../areas/get_all_areas",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allArea = mydata.data.allArea;
            $scope.areaS = mydata.data.allArea[0].id;
        });
    }

    $scope.get_all_areas();
//--------------------------------------------------------
    $scope.get_all_agents = function () {
        $http({
            url: "../agents/get_all_agents",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allAgents = mydata.data.allAgents;
        });
    }

    $scope.get_all_agents();
//---------------------------------------------------------
    $scope.get_all_dele = function () {
        $http({
            url: "../delegators/get_all_delegators",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allDelegators = mydata.data.allDelegators;
        });
    }

    $scope.get_all_dele();
//--------------------------------------------------------
    $scope.get_all_cType = function () {
        $http({
            url: "../clients/get_all_types",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allTypes = mydata.data.allTypes;

            $scope.cType = mydata.data.allTypes[0].id;

        });
    }

    $scope.get_all_cType();
//---------------------------------------------------------
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
    $scope.get_all_ct = function () {
        $http({
            url: "../clients/get_all_clients2",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients2 = mydata.data.allClients;
        });
    }
    $scope.get_all_ct();
//========================================================
    var page = 0;
    $scope.pageNum = 1;
    var pageCount;

    $scope.get_all=function()
    {
        $http({
            url: "../clients/get_all_clients?page="+page,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
            $scope.pageCount = mydata.data.pageCount;
            pageCount=mydata.data.pageCount;
        });
        chechBtn();
    }

    $scope.get_all(); // to get all ...
//=================================================
    $scope.get_next = function () {
        page +=1;
        $scope.pageNum = page + 1;
        $http({
            url: "../clients/get_all_clients?page="+page,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
        });

        chechBtn();
    }
//================================================
    $scope.get_previous = function () {
        page -= 1;
        $scope.pageNum = $scope.pageNum -1;

        $http({
            url: "../clients/get_all_clients?page="+page,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClients = mydata.data.allClients;
        });

        chechBtn();
    }
//================================================
    function chechBtn()
    {
        if ($scope.pageNum == pageCount) {
            document.getElementById("btn_next").disabled = true;
        } else {
            document.getElementById("btn_next").disabled = false;
        }

        if ($scope.pageNum == 1) {
            document.getElementById("btn_pre").disabled = true;
        }
        else {
            document.getElementById("btn_pre").disabled = false;
        }
    }
//==================================================
    var parentId = 0;
    $scope.choose=function(id,name)
    {
        parentId = id;
        $scope.pName = name;
    }
//=================================================
    $scope.choose2 = function (name) {
        $scope.pName2 = name;
    }
//==================================================

    $scope.add_client = function () {

        if ($scope.name == null || parentId == 0)
        {
            alert("لم تتم عمليه الاضافه .. لا يمكن ان تترك قيمه اسم العميل والعميل الرئيسي فارغه ");
        }
        else
        {
                $http({
                    url: "../clients/add_client",
                    method: "POST",
                    data: {
                        cltTypeId: $scope.cType,
                        gender:document.getElementById("gender").value,
                        jop: $scope.jop,
                        DOB: $scope.DOB,
                        idNum: $scope.idNum,
                        name: $scope.name,
                        parentId: parentId,
                        phone1: $scope.ph1,
                        shreha1: $scope.sh1,
                        phone2: $scope.ph2,
                        shreha2: $scope.sh2,
                        phone3: $scope.ph3,
                        shreha3: $scope.sh3,
                        areaId: $scope.areaS,
                        getPlace: $scope.getPlace,
                        workPlace: $scope.workPlace,
                        workName: $scope.workName,
                        flat: $scope.flat,
                        floor: $scope.floor,
                        stayPlace: $scope.stayPlace,
                        delegatorId: $scope.deleS,
                        idPlace: $scope.idPlace,
                        contractDate: $scope.contractDate,
                        notes: $scope.notes,
                        chargeMsg: $scope.chargeMsg
                    }
                }).then
                (function (mydata) {

                    document.getElementById("addBtn").disabled = true;

                    $scope.get_all();
                    alert(mydata.data.msg);
                },
                function(errorReason)
                {
                    alert("an error occurs" + errorReason.data);
                });
            
        }
    }
//=======================================================
    $scope.makeAddPossible = function (c)
    {
        document.getElementById("addBtn").disabled = false;
    }
//======================================================
    var clientId = 0;

    $scope.update = function (c) {

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

        if (c.done == 1)
        {
            $scope.done = true;
        }else
        {
            $scope.done = false;
        }   
    }
//======================================================
    $scope.loadPhones = function () {
        $http({
            url: "../clients/load_client_phones?cId=" + clientId,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allClientPhones = mydata.data.allClientPhones;
        });
    }
//======================================================

    $scope.addPhone = function () {

        if ($scope.adPhone == null || $scope.adPhone == "" || $scope.adShareha==null) {
            alert("اكمل البيانات من فضلك");
        }
        else if ( isNaN($scope.adPhone)) {
            alert("رقم التليفون غير صحيح");
        }
        else {

            $http({
                url: "../clients/add_phone_to_client",
                method: "POST",
                data: {
                    cId: clientId,
                    phone: $scope.adPhone,
                    sharehaId: $scope.adShareha
                }
            }).then
            (function (mydata) {
                alert(mydata.data.msg);
                $scope.adPhone = null;
            });
        }
    }
//=====================================================

    $scope.updatePhone = function (phoneId , index) {

        if (document.getElementById("ph" + index).value == null || document.getElementById("ph" + index).value == "" || document.getElementById("cart" + index).value == null || document.getElementById("cart" + index).value=="") {
            alert("اكمل البيانات من فضلك");
        }
        else if (isNaN(document.getElementById("ph"+index).value)) {
            alert("رقم التليفون غير صحيح");
        }
        else {
            $http({
                url: "../clients/update_phone_of_client",
                method: "POST",
                data: {
                    phId: phoneId,
                    phone: document.getElementById("ph" + index).value,
                    sharehaId: document.getElementById("cart" + index).value
                }
            }).then
            (function (mydata) {
                alert(mydata.data.msg);
            });
        }
    }
//======================================================
    $scope.deletePhone = function (phoneId)
    {
        $http({
            url: "../clients/delete_phone?phId="+phoneId,
            method: "POST",
          
        }).then
           (function (mydata) {
               alert(mydata.data.msg);
           });
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
                    cId:clientId,
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
            url: "../clients/get_client_phones?cId="+clientId,
            method: "GET",
        }).then
        (function (mydata) {
            // $scope.allClients = mydata.data.allClients;

            if (mydata.data.phones[0] != null)
            {
                $scope.ph1_2 = mydata.data.phones[0].number;
                $scope.sh1_2 = mydata.data.phones[0].sharehaId;
            }
            else
            {
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
    $scope.getClient=function(id)
    {
        clientId = id;
    }

//======================================================
    $scope.delete = function () {

        $http({
            url: "../clients/delete_client",
            method: "POST",
            data: { cId: clientId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//======================================================
    $scope.makeDone = function () {
        $http({
            url: "../clients/make_done?clientId="+clientId,
            method: "POST",
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//======================================================
    var cid_note = 0;
    var cname_note;
    $scope.get_client_notes = function (id, name) {

        cid_note = id;
        cname_note = name;

        $scope.cname_n = name;

        $http({
            url: "../clients/all_client_notes?cId="+id,
            method: "GET",
        }).then
        (function (mydata) {
            $scope.notes = mydata.data.allNotes;
            $scope.note_details = "";
        });
    }
//====================================================
    $scope.get_note_details= function (note) {
        $scope.note_details = note;
    }
//===========================================
    var noteId = 0;
    $scope.get_note_id = function (id) {
        noteId = id;
    }
//============================================
    $scope.delete_note = function () {

        $http({
            url: "../clients/delete_note",
            method: "POST",
            data: { noteId: noteId },
        }).then
        (function (mydata) {
            $scope.get_client_notes(cid_note,cname_note);
            alert(mydata.data.msg);
        });
    }
//====================================================
    $scope.add_note = function () {

        if ($scope.note_details == "")
        {
            alert("لا يمكن ان تضيف قيمه فارغه");
        }
        else {
            $http({
                url: "../clients/add_note",
                method: "POST",
                data: { cId: cid_note, note: $scope.note_details },
            }).then
            (function (mydata) {
                $scope.get_client_notes(cid_note, cname_note);
                alert(mydata.data.msg);
            });
        }
    }
//====================================================
    $scope.get_total_ops_areas = function () {

        $http({
            url: "../clients/get_clients_summarization?sDate=" + $scope.from + "&eDate=" + $scope.to,
            method: "GET",
        }).then
        (function (mydata) {
            var cList = mydata.data.summariseList;
            var areaList = mydata.data.areaIds;
            var areaSummerizationList = [];
           

            for (var i = 0; i < areaList.length; i++)
            {
                var charge_first = 0;
                var sales = 0;
                var tahseel = 0;
                var badal = 0;
                var omolat = 0;
                var charge_last = 0;
                var areaSummerizationObj = {};
               
                for (var j = 0; j < cList.length; j++) 
                {
                    if (cList[j].Data.areaId == areaList[i])
                    {
                        areaSummerizationObj.areaId = cList[j].Data.areaId;
                        areaSummerizationObj.areaName = cList[j].Data.areaName;

                        charge_first += parseFloat(cList[j].Data.actual_charge_start);
                        sales += parseFloat(cList[j].Data.sales);
                        tahseel += parseFloat(cList[j].Data.tahseel);
                        badal += parseFloat(cList[j].Data.badal_tahseel);
                        omolat += parseFloat(cList[j].Data.amola);
                        charge_last += parseFloat(cList[j].Data.actual_charge);
                    }                                
                }

                if (areaSummerizationObj.areaName!=null)
                {
                    areaSummerizationObj.charge_first = charge_first;
                    areaSummerizationObj.sales = sales;
                    areaSummerizationObj.tahseel = tahseel;
                    areaSummerizationObj.badal = badal;
                    areaSummerizationObj.omolat = omolat;
                    areaSummerizationObj.charge_last = charge_last;
                    areaSummerizationList.push(areaSummerizationObj);
                }         
            }
            //for (var i = 0; i < areaSummerizationList.length; i++)
            //{
            //    alert(areaSummerizationList[i].areaName);
            //    alert(areaSummerizationList[i].sales);
            //    alert(areaSummerizationList[i].tahseel);
            //    alert(areaSummerizationList[i].badal);
            //    alert(areaSummerizationList[i].omolat);
            //    alert(areaSummerizationList[i].charge_last);
            //    alert("-------------------------------");
            //}

            $scope.areaSummerizationList = areaSummerizationList;

        });

    }
    $scope.get_total_ops_areas();
//====================================================
    $scope.get_total_ops_clients = function () {
            $http({
                url: "../clients/get_clients_summarization?sDate=" + $scope.from + "&eDate=" + $scope.to,
                method: "GET",
            }).then
            (function (mydata) {
                $scope.summariseList = mydata.data.summariseList;
            });
    }
    $scope.get_total_ops_clients();
//======================================================
    $scope.get_summerize_clts = function ()
    {
        $scope.charge_first = charge_first;
        $scope.sales = sales;
        $scope.tahseel = tahseel;
        $scope.badal = badal;
        $scope.omolat = omolat;
        $scope.charge_last = charge_last;
    }
//----------------------------------------------------
    $scope.get_summerize_area = function () {
        $scope.charge_first = charge_first2;
        $scope.sales = sales2;
        $scope.tahseel = tahseel2;
        $scope.badal = badal2;
        $scope.omolat = omolat2;
        $scope.charge_last = charge_last2;
    }
//=======================================================
    $scope.get_all_clt_permissions = function () {
        $http({
            url: "../reports/get_all_permissions",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allPermissions = mydata.data.allPermissions;
        });
    }
    $scope.get_all_clt_permissions();
//=======================================================
    var reportId = 0;
    $scope.get_report = function (id)
    {
        reportId = id;
    }
//=======================================================
    $scope.delete_report = function () {

        $http({
            url: "../reports/delete_clt_permission",
            method: "POST",
            data: { id: reportId },
        }).then
        (function (mydata) {
            $scope.get_all_clt_permissions();
            alert(mydata.data.msg);
        });
    }
//=====================================================
    //$scope.print = function () {

    //    $http({
    //        url: "../reports/اذن استلام-pdf",
    //        method: "GET",
    //       // data: { id: reportId },
    //    }).then
    //    (function (mydata) {
          
    //    });
    //}




}); //end controller

angular.module('myApp').filter('myDateFilter', function () {

    return function (contract_date) {
       
        var dateTokens = contract_date.split("/");

        var day = parseInt(dateTokens[0]);
        var month = parseInt(dateTokens[1])-1;
        var year = parseInt(dateTokens[2]);

         var jsDate = new Date(year,month,day);

         return jsDate;
    };


});
//=======================================================

function get_js_date(date)
{
    var dateTokens = date.split("/");

    var day = parseInt(dateTokens[0]);
    var month = parseInt(dateTokens[1]) - 1;
    var year = parseInt(dateTokens[2]);

    var jsDate = new Date(year, month, day);

    return jsDate;
}

//=======================================================
angular.module('myApp').filter('myFilter', function () {

    return function (allClients,from,to) {

        var result = [];
        
        for (var i = 0; i < allClients.length; i++)
        {
            var date = get_js_date(allClients[i].contract_date);

            if (from !=null && date < from)
            {
                result.push(allClients[i]);
            }
        }
        return allClients;
    };
});

//=======================================================
angular.module('myApp').filter('mySumFilter', function () {

    return function (List) {

        charge_first = 0;
        sales = 0;
        tahseel = 0;
        badal = 0;
        omolat = 0;
        charge_last=0;
       
        for (var i = 0; i < List.length; i++) {

            charge_first += parseFloat(List[i].Data.actual_charge_start);
            sales += parseFloat(List[i].Data.sales);
            tahseel += parseFloat(List[i].Data.tahseel);
            badal += parseFloat(List[i].Data.badal_tahseel);
            omolat += parseFloat(List[i].Data.amola);
            charge_last += parseFloat(List[i].Data.actual_charge);
        }

        return List;
    };
});
//============================================================
angular.module('myApp').filter('mySumAreaFilter', function () {

    return function (List) {

        charge_first2 = 0;
        sales2 = 0;
        tahseel2 = 0;
        badal2 = 0;
        omolat2 = 0;
        charge_last2 = 0;

        for (var i = 0; i < List.length; i++) {
            charge_first2 += parseFloat(List[i].charge_first);
            sales2 += parseFloat(List[i].sales);
            tahseel2 += parseFloat(List[i].tahseel);
            badal2 += parseFloat(List[i].badal);
            omolat2 += parseFloat(List[i].omolat);
            charge_last2 += parseFloat(List[i].charge_last);
        }

        return List;
    };
});



