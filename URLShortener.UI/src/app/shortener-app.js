(function () {
    'use strict';
    var app = angular.module('shortener-app',
    [
        // Angular modules 
        'ui.router',
        'ngAria',
        'LocalStorageModule',
        'ngMessages',
        'shortener'
    ]);
})();