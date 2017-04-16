(function () {
    'use strict';

    var serviceId = 'util';

    function utilService() {
        return {
            convertJsonDate: function (date) {
                if (date == null || date == '')
                    return '';
                return moment(date).format('DD.MM.YYYY HH:mm');
            }
        }
    }

    angular.module('shortener-app').factory(serviceId, utilService);
})();