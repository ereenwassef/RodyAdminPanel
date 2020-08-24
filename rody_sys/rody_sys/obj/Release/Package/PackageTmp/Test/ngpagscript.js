
/* ------------------------------------------------------- 

* Filename:     AngularJS Pagination
* Website:      http://www.shanidkv.com
* Description:  Shanidkv AngularJS blog
* Author:       Shanid KV shanidkannur@gmail.com

---------------------------------------------------------*/

var app = angular.module('shanidkvApp',[]);
 


function githubController($scope,$http) {
    $http.get("https://api.github.com/users/mralexgray/repos")
    .success(function(response) {$scope.names = response;});

    $scope.currentPage = 0;
    $scope.pageSize = 5;
    $scope.numberOfPages=function(){
        return Math.ceil($scope.names.length/$scope.pageSize);                
    }
}

//startFrom filter

app.filter('firstPage', function() {
    return function(input, start) {
        start = +start;
        return input.slice(start);
    }
});