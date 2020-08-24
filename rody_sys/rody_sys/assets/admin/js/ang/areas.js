/// <reference path="../angular.js" />

//var app = angular.module("myApp", []);

angular.module('myApp').controller("ctr_area", function ($scope, $http) {


    $scope.get_all=function()
    {
        $http({
            url: "../areas/get_all_areas",
            method: "GET",
        }).then
        (function (mydata) {
            $scope.allArea = mydata.data.allArea;
        });
    }

    $scope.get_all(); // to get all ...
    //==================================================

    var areaId = 0;

    $scope.get_area = function (id) {

        areaId = id;
    }
    //=================================================
    $scope.delete = function () {
        $http({
            url: "../areas/delete_area",
            method: "POST",
            data: { id: areaId },
        }).then
        (function (mydata) {
            $scope.get_all();
            alert(mydata.data.msg);
        });
    }
    //================================================

    $scope.add = function () {


        if($scope.name==null || $scope.gname==null)
        {
            alert("لم تتم الاضافه .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name)) {
            alert("لم تتم الاضافه .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../areas/add_area",
                method: "POST",
                data: { name: $scope.name, governId: $scope.gname },
            }).then
            (function (mydata) {
                $scope.name = "";
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }
    //===============================================
       

    $scope.update=function(id,name,gname)
    {
        areaId = id;

        //alert(id + " " + name + " " + gname);

        $scope.name2 = name;
        document.getElementById("gname2").value = gname;

    }

    //===============================================
    $scope.updateDB = function () {
        
        var gov = document.getElementById("gname2").value;

        if ($scope.name2 == null || gov=="" ) {
            alert("لم يتم العديل  .. لا يمكن ان تدخل قيم فارغه");
        } else if (!isNaN($scope.name2)) {
            alert("لم يتم العديل .. حاول ان تدخل قيمه صحيحه");
        }
        else {

            $http({
                url: "../areas/update_area",
                method: "POST",
                data: {id:areaId, name: $scope.name2, governId: gov },
            }).then
            (function (mydata) {
                $scope.get_all();
                alert(mydata.data.msg);
            });
        }
    }

//================================================
    $scope.icon_id = "fa-caret-up"; //default
    $scope.icon_name = "";
    $scope.icon_gname = "";

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
            $scope.icon_gname = "";


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
            $scope.icon_gname = "";


        }
        else if (col == "gname") {

            if (previous_icon == "up") {
                $scope.icon_gname = "fa-caret-down";
                $scope.sortedCol = "-gname";

                previous_icon = "down";
                previous_col = col;

            }
            else {
                $scope.icon_gname = "fa-caret-up";

                $scope.sortedCol = "+gname";
                previous_icon = "up";
            }


            $scope.icon_id = "";
            $scope.icon_name = "";


        }
    }





//=======================================================
}); //end controller