(function () {

    'use strict';

    angular.module('shortener').factory('shortenerService', ['$http', 'localStorageService', function ($http, localStorage) {
        //localStorage 
        //чтобы при обновлении страницы не пропадал архив ссылок
        var userGuid = null;
        function shortenerUrl(model) {
            var params = {
                data: {
                    OriginUrl: model.orignUrl,
                    UserGuid: getUserGuid()
                }
            };
            return $http.post('/api/Shortener/ConvertOrignUrl', params);
        }

        function getShortenerArchive() {
            return $http.get('/api/Shortener/GetShortenerArchive?userGuid=' + getUserGuid());
        }

        function getUserGuid(){
            userGuid = localStorage.get('userGuid');
            return userGuid ? userGuid : null;
        }

        function setUserGuid(guid) {
            if (localStorage.isSupported) {
                localStorage.set('userGuid', guid);
            }
        }

        return {
            shortenerUrl: shortenerUrl,
            getShortenerArchive: getShortenerArchive,
            setUserGuid: setUserGuid
        };
    }]);
})();