using AbpFramework.Runtime.Caching;
using System.Web.Mvc;
using AbpDemo.Application.Users;
using AbpDemo.Application.Test;
using Abp.Web.Mvc.Web.Mvc.Authorization;
namespace AbpDemo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ABPFrameworkDemoControllerBase
    {
        //private readonly ITestService _testService;
        private readonly ICacheManager _cacheManager;
        private readonly IUserAppService _userAppService;        
        public ITestService _testService { get; set; }
        public HomeController(ICacheManager cacheManager, IUserAppService userAppService)
        {
            //this._testService = testService;
            this._cacheManager = cacheManager;
            this._userAppService = userAppService;
        }
        public ActionResult Index()
        {
            var values = _userAppService.GetAll();
            var _cache=_cacheManager.GetCache("hello");
            var userList = _cacheManager.GetCache("hello").Get
                ("hello", lambda =>
                {
                   return _testService.GetAll();
                    } );

            //var userList = _cacheManager.GetCache("ControllerCache").Get("AllUsers", () => _userAppService.GetUsers());
            var value = _cache.GetOrDefault("hello");
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