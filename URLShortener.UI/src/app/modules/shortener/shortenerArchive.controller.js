(function () {
    'use strict';
    var controllerId = 'ShortenerArchiveController';

    var shortenerArchive = function (util, shortenerService) {
        var vm = this;

        //PARAMETER

        //METHOD
        vm.convertDateTime = convertDateTime;


        init();

        function init() {
            var promise = shortenerService.getShortenerArchive();
            promise.then(function successCallback(result) {
                if (result != null)
                    vm.archives = result.data;
                else
                    vm.archives = [];
            },
                function fail() {
                    alert('Произошла ошибка, пожалуйста, попробуйте позже');
                });
        }

        function convertDateTime(dt) {
            return util.convertJsonDate(dt);
        }
    }

    shortenerArchive.$inject = ['util', 'shortenerService'];
    angular.module('shortener').controller(controllerId, shortenerArchive);

})();