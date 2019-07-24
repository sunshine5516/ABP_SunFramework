(function () {
    var controllerId = 'app.views.layout.header';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;
            //var vm = appSession;
            //vm.languages = abp.localization.languages;
            //vm.currentLanguage = abp.localization.currentLanguage;

            //vm.menu = abp.nav.menus.MainMenu;
            vm.currentMenuName = $state.current.menu;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                vm.currentMenuName = toState.menu;
            });

            vm.getShownUserName = function () {
                if (!abp.multiTenancy.isEnabled) {
                    return appSession.user.UserName;
                } else {
                    if (appSession.tenant) {
                        return appSession.tenant.TenancyName + '\\' + appSession.user.UserName;
                    } else {
                        return '.\\' + appSession.user.UserName;
                    }
                }
            };

            //vm.getShownUserName = function () {
            //        return appSession.user;   
            //};
            
        }
    ]);
})();