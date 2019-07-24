(function () {
    angular.module('app').factory('appSession', [
            function () {

                var _session = {
                    user: null,
                    tenant: null
                };

                abp.services.app.session.getCurrentLoginInformations({ async: false }).done(function (result) {
                    _session.user = result.User;
                    _session.tenant = result.Tenant;
                });

                return _session;
            }
        ]);
})();