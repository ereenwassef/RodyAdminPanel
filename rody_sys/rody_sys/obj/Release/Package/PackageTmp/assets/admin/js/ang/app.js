/// <reference path="angular.js" />


var app = angular.module('myApp', [
        'ngRoute',
        'ui.bootstrap'
]);

//var app = angular.module("myApp", ['ngRoute', 'ui.bootstrap']);

// '/page_1' = '/page_1'


app.config(["$routeProvider", function ($routeProvider, $routeParams) {

    $routeProvider.
      
    when('/agents/getAllAgents', {
        templateUrl: '~/agents/getAllAgents',
        controller: 'ctr_agent'
    }).
    when('/areas/getAllAreas', {
        templateUrl: '../areas/getAllAreas',
        controller: 'ctr_area'
    }).
     when('/clients/clientHistory/:id', {
         templateUrl: function (params) { return '../clients/clientHistory?' + params.id; },
         controller: 'ctr_clientH'
     }).
        when('/clients/clientHistory', {
            templateUrl: function (params) { return '../clients/clientHistory'; },
            controller: 'ctr_clientH'
        }).
      when('/clients/clientTree/:cId', {
          templateUrl: function (params) { return '../clients/clientTree?' + params.cId; },
          controller: '',
      }).
     when('/clients/getAllClients', {
         templateUrl: '~/clients/getAllClients',
         controller: 'ctr_client'
     }).
     when('/clients/getAllClientsCharge', {
         templateUrl: '../clients/getAllClientsCharge',
         controller: 'ctr_clientC'
     }).
      when('/delegators/getAllDelegators', {
          templateUrl: '~/delegators/getAllDelegators',
          controller: 'ctr_dele'
      }).
     when('/governs/getAllGoverns', {
         templateUrl: '../governs/getAllGoverns',
         controller: 'ctr_govern'
     }).
     when('/sales/addSale', {
         templateUrl: '../sales/addSale',
         controller: 'ctr_sale'
    }).
      when('/sales/getAllSales', {
          templateUrl: '../sales/getAllSales',
          controller: 'ctr_sale2'
    }).
    when('/treasuries/addTahseel', {
        templateUrl: '../treasuries/addTahseel',
        controller: 'ctr_tahseel'
    }).
     when('/client/clientTypes', {
         templateUrl: '../clients/clientTypes',
         controller: 'c_types'
     }).
     when('/suppliers/mainCategories', {
         templateUrl: '../suppliers/mainCategories',
         controller: 'suppliers_main'
     }).
        when('/suppliers/subCategories', {
            templateUrl: '../suppliers/subCategories',
            controller: 'suppliers_sub'
        }).
     when('/suppliers/getAllSuppliers', {
         templateUrl: '../suppliers/getAllSuppliers',
         controller: 'suppliers'
     }).
        when('/banks/getAllBanks', {
            templateUrl: '../banks/getAllBanks',
            controller: 'banks'
        }).
         when('/partners/getAllPartners', {
             templateUrl: '../partners/getAllPartners',
             controller: 'partners'
         }).
        when('/treasuries/getAllTreasuries', {
            templateUrl: '../treasuries/getAllTreasuries',
            controller: 'treasuries'
        }).
        when('/treasuries/treasuriesOperations', {
            templateUrl: '../treasuries/treasuriesOperations',
            controller: 'treasuries'
        }).
         when('/stores/getAllStores', {
             templateUrl: '../stores/getAllStores',
             controller: 'stores'
         }).
         when('/purchases/getAllPurchases', {
             templateUrl: '../purchases/getAllPurchases',
             controller: 'purchases'
         }).
         when('/purchases/addPurchaseOperation', {
             templateUrl: '../purchases/addPurchaseOperation',
             controller: 'purchases'
         }).
        when('/services/getAllServices', {
            templateUrl: '../services/getAllServices',
            controller: 'services'
        }).
         when('/suppliers/supplierHistory', {
             templateUrl: '../suppliers/supplierHistory',
             controller: 'suppliers'
         }).
        when('/clients/clientsSummarization', {
            templateUrl: '../clients/clientsSummarization',
            controller: 'ctr_client'
        }).
          when('/clients/areasSummarization', {
              templateUrl: '../clients/areasSummarization',
              controller: 'ctr_client'
          }).
         when('/clients/clientsMostahekElsadad', {
             templateUrl: '../clients/clientsMostahekElsadad',
             controller: 'ctr_client'
         }).
          when('/services/servicesChart', {
              templateUrl: '../services/servicesChart',
              controller: 'services'
          }).
          when('/sales/phoneNumHistory', {
              templateUrl: '../sales/phoneNumHistory',
              controller: 'ctr_sale2'
          }).
    otherwise({
        templateUrl: 'intro'
    });


}]);

//============================== ** custom filters ** ============================================

function get_js_date(date) {

    var dateTokens = date.split("/");

    var day = parseInt(dateTokens[0]);
    var month = parseInt(dateTokens[1]) - 1;
    var year = parseInt(dateTokens[2]);

    var jsDate = new Date(year, month, day);

    return jsDate;
}
//=========================================================
function get_js_date_from_to(date) {

    var dateTokens = date.split("-");

    var day = parseInt(dateTokens[2]);
    var month = parseInt(dateTokens[1]) - 1;
    var year = parseInt(dateTokens[0]);

    var jsDate = new Date(year, month, day);

    return jsDate;
}
//=======================================================
angular.module('myApp').filter('myGenericDateFilter', function () {
    return function (list, from, to) {

      //  alert("aaa");

        if (from || to) {

            var result = [];

            for (var i = 0; i < list.length; i++) {

                var date = get_js_date(list[i].date);

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
//=========================================================================
angular.module('myApp').filter('myActualDateFilter', function () {
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
//=========================================================================
angular.module('myApp').filter('myContractDateFilter', function () {
    return function (list, from, to) {

        if (from || to) {

            var result = [];

            for (var i = 0; i < list.length; i++) {

                var date = get_js_date(list[i].contract_date);

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
//==========================================================================
angular.module('myApp').filter('myActivationDateFilter', function () {
    return function (list, from, to) {

        if (from || to) {

            var result = [];

            for (var i = 0; i < list.length; i++) {

                var date = get_js_date(list[i].activation_date);

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
//==========================================================================
angular.module('myApp').filter('sumOfSalesFilter', function () {
    return function (list) {
        var sum = 0;
        for (var i = 0; i < list.length; i++) {

            sum += list[i].total;
        }
        return sum;
    }                     
});
//==========================================================================
angular.module('myApp').filter('mySliceFilter', function () {
    return function (list, start, end) {

       // alert(list.length);

        if (start || end) {
            if (start == null || start == "" && end != null)
            {
              //  alert("end=" + end);

                return list.slice(0, end);
            }
            else if (end == null || end == "" && start != null)
            {
                return list.slice(start-1, list.length);
            }
            else if (start != null && end != null)
            {
                return list.slice(start-1, end);
            }          
        }
        else
        {
            return list;
        }

    }
});
//==========================================================================
angular.module('myApp').filter('myPhoneFilter', function () {
    return function (list, phone) {

        if (phone !="" && phone !=null) {

            var result = [];
            for (var i = 0; i < list.length; i++)
            {
                if (list[i].number != null)
                {
                    if (list[i].number.includes(phone)) {
                        result.push(list[i]);
                    }
                } else if (list[i].otherPhoneNum != null)
                {
                    if (list[i].otherPhoneNum.includes(phone)) {
                        result.push(list[i]);
                    }
                }
            }

            return result;
        }
        else
        {
            return list;
        }
    }
});
//==========================================================================
angular.module('myApp').filter('myRangeFilter', function () {
    return function (list, start,end) {

        if (start != null  || end != null ) {

            var result = [];
            for (var i = 0; i < list.length; i++)
            {               
                if (start != null && end != null)
                {
                    if (list[i].id <= end && list[i].id>=start)
                    {
                        result.push(list[i]);
                    }
                }
                else if (start != null && end == null) {
                    if (list[i].id >= start) {
                        result.push(list[i]);
                    }
                }
                else if (start == null && end != null) {
                    if (list[i].id <= end ) {
                        result.push(list[i]);
                    }
                }
            }

            return result;
        }
        else {
            return list;
        }
    }
});
