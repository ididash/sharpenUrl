(function () {
    "use strict";


    function locationProvider($locationProvider) {
        $locationProvider.html5Mode(true);
    };


    function routing($stateProvider, $urlRouterProvider) {
        $stateProvider
           .state('/', {
               url: '/',
               views: {
                   "root": {
                       templateUrl: '../src/app/modules/shortener/html/index.html',
                       controller: "ShortenerController",
                       controllerAs: "vm"
                   }
               },
               styleType: 0
           }).state('archive', {
               url: '/archive',
               views: {
                   "root": {
                       templateUrl: '../src/app/modules/shortener/html/archive.html',
                       controller: "ShortenerArchiveController",
                       controllerAs: "vm"
                   }
               },
               styleType: 0
           });


        $urlRouterProvider
            .otherwise("/");
    };

    locationProvider.$inject = ['$locationProvider'];
    routing.$inject = ['$stateProvider', '$urlRouterProvider'];

    angular.module('shortener-app').config(locationProvider).config(routing);
}());