/// <reference path="../../../general/js/angular.js" />

var app = angular.module("myApp", []);
app.controller("ctr_govern", function ($scope, $http) {

    $scope.currentPage = 1;
    $scope.numPages = 0;
    $scope.pageSize = 5;
    $scope.start = 0;

    var length=0;
    $scope.get_all=function()
    {

        $http({
            url: "../governs/get_all_govern",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allGovern = mydata.data.allGovern;

            length = mydata.data.allGovern.length;
            $scope.numPages = Math.ceil(length / $scope.pageSize);

        });

    }

    $scope.get_all(); // to get all ...
//========================================================
    //******* paging ********

    $scope.get_next = function ()
    {
        $scope.check_disabled();

        $scope.start += 5;
        $scope.currentPage += 1;
        $scope.check_disabled();
    }
//-------------------------
    $scope.get_previous = function () {
        $scope.check_disabled();

        $scope.start -= 5;
        $scope.currentPage -= 1;
        $scope.check_disabled();
    }
//-------------------------
    $scope.check_disabled = function ()
    {
        if ($scope.start == 0) {
            document.getElementById("btn_pre").disabled = true;
            if($scope.start<length)
            {
                document.getElementById("btn_next").disabled = false;
            }
        }
        else if($scope.start>=length-$scope.pageSize)
        {
            document.getElementById("btn_next").disabled = true;
        }
        else
        {
            document.getElementById("btn_pre").disabled = false;
        }
    }
    $scope.check_disabled();

//=========================================================

    $scope.add = function () {
        if ($scope.govName==null)
        {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        }
        else if (!isNaN($scope.govName)) {
            alert("لم تتم الاضافه .. حاول ان تدخل قيمه صحيحه");
        }
        else {
            $http({
                url: "../governs/add_govern",
                method: "POST",
                data: { govern_name: $scope.govName }
            }).then
            (function (mydata) {

                $scope.govName = "";
                $scope.get_all();
                alert(mydata.data.msg);

            });
        }
    }
//========================================================
    var governId = 0;

    $scope.get_govern = function (id) {

        governId = id;
    }
//========================================================
    $scope.delete = function () {
        $http({
            url: "../governs/delete_govern",
            method: "POST",
            data: { id: governId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//======================================================
    $scope.update_govern = function (id,name) {

        governId = id;

        $scope.gName = name;
    }
//======================================================
    $scope.updateDB = function () {
        $http({
            url: "../governs/update_govern",
            method: "POST",
            data: { id: governId , name:$scope.gName},
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
//======================================================
    $scope.icon_id = "fa-caret-up"; //default
    $scope.icon_name = "";

    $scope.sortedCol = "+id"; //default

    var previous_col = "id";
    var previous_icon = "up";

    $scope.sort = function (col) {
        previous_col = col;

        if (col == "id") {
                if (previous_icon == "up") {
                    $scope.icon_id = "fa-caret-down";
                    $scope.sortedCol = "-id";

                    previous_icon = "down";

                }
                else {
                    $scope.icon_id = "fa-caret-up";

                    $scope.sortedCol = "+id";
                    previous_icon = "up";
                }
            

            $scope.icon_name = "";
          

        } else if (col == "name") {

                if (previous_icon == "up") {
                    $scope.icon_name = "fa-caret-down";
                    $scope.sortedCol = "-name";

                    previous_icon = "down";
                    previous_col = col;

                }
                else {
                    $scope.icon_name = "fa-caret-up";

                    $scope.sortedCol = "+name";
                    previous_icon = "up";
                }
            

            $scope.icon_id = "";
          

        }
    }

//=======================================================
}); //end controller