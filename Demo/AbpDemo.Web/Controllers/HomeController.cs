using AbpFramework.Runtime.Caching;
using System.Web.Mvc;
using AbpDemo.Application.Users;
using AbpDemo.Application.Test;

namespace AbpDemo.Web.Controllers
{
    public class HomeController : ABPFrameworkDemoControllerBase
    {
        private readonly AbpDemo.Application.Test.ITestService _testService;
        private readonly ICacheManager _cacheManager;
        private readonly IUserAppService _userAppService;

        public HomeController(ITestService testService
            ,ICacheManager cacheManager, IUserAppService userAppService)
        {
            this._testService = testService;
            this._cacheManager = cacheManager;
            this._userAppService = userAppService;
        }
        public ActionResult Index()
        {
            var values = _userAppService.GetAll();
            var _cache=_cacheManager.GetCache("hello");
            var value= _cache.GetOrDefault("hello");
            _cacheManager.GetAllCaches();
            //TestService _testService = new TestService();
            _testService.GetTestMethod();
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}