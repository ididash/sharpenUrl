(function () {
    'use strict';
    var controllerId = 'ShortenerController';

    var shortener = function (shortenerService) {
        var vm = this;

        //PARAMETER
        vm.shortener = {};

        //METHOD
        vm.shortenerUrl = shortenerUrl;


        function shortenerUrl() {
            if (!vm.shortenerForm.$valid) {
                if (vm.shortenerForm.$invalid) {
                    angular.forEach(vm.shortenerForm.$error,
                        function (field) {
                            angular.forEach(field,
                                function (errorField) {
                                    try {
                                        errorField.$setTouched();
                                    } catch (e) {
                                        // continue regardless of error
                                    }
                                });
                        });
                }
                return;
            }

            //тут было бы неплохо запустить loader
            var promise = shortenerService.shortenerUrl(vm.shortener);
            promise.then(function successCallback(result) {
                    //а тут остановить loader
                    if (result.data != null) {
                        vm.shortener = {
                            orignUrl: '',
                            compactUrl: result.data.CompactUrl
                        }
                        //alert('Ваша ссылка успешно изменена! Новая ссылка: ' + result.data.CompactUrl);
                        shortenerService.setUserGuid(result.data.UserGuid);
                        vm.shortenerForm.$setUntouched();
                    }
                    else {
                        alert('Произошла ошибка, пожалуйста, попробуйте позже');
                    }
                },
                function fail() {
                    alert('Произошла ошибка, пожалуйста, попробуйте позже');
                    //а тут остановить loader
                });
        }
    }

    shortener.$inject = ['shortenerService'];
    angular.module('shortener').controller(controllerId, shortener);

})();