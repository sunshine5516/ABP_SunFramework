using AbpDemo.Application.Configuration.Ui;
using AbpDemo.Application.Sessions;
using AbpDemo.Core.Configuration;
using AbpDemo.Web.Models;
using AbpDemo.Web.Models.Layout;
using AbpFramework.Application.Navigation;
using AbpFramework.Configuration;
using AbpFramework.Threading;
using System.Linq;
using System.Web.Mvc;
using AbpFramework.Runtime.Session;
namespace AbpDemo.Web.Controllers
{
    public class LayoutController : ABPFrameworkDemoControllerBase
    {
        #region 声明实例
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        #endregion
        #region 构造函数
        public LayoutController(IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService)
        {
            this._userNavigationManager = userNavigationManager;
            this._sessionAppService = sessionAppService;
        }
        #endregion
        #region 方法
        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult RightSideBar()
        {
            var themeName = SettingManager.GetSettingValue(AppSettingNames.UiTheme);
            var viewModel = new RightSideBarViewModel
            {
                CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            };
            return PartialView("_RightSideBar", viewModel);
        }
        [ChildActionOnly]
        public PartialViewResult SideBarNav(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => 
                _userNavigationManager.GetMenuAsync("MainMenu",AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };
            return PartialView("_SideBarNav", model);
        }
        [ChildActionOnly]
        public PartialViewResult SideBarUserArea()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.
                  GetCurrentLoginInformations()),
                IsMultiTenancyEnabled = true
            };
            return PartialView("_SideBarUserArea", model);
        }
        #endregion

    }
}