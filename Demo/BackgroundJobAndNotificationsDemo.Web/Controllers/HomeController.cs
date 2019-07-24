using Abp.Web.Mvc.Web.Mvc.Authorization;
using AbpDemo.Application.Emailing;
using AbpDemo.Application.Users;
using AbpFramework.Application.Services.Dto;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BackgroundJobAndNotificationsDemo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BackgroundJobAndNotificationsDemoControllerBase
    {
        private readonly IPrivateEmailAppService _privateEmailAppService;
        private readonly IUserAppService _userAppService;
        public HomeController(IPrivateEmailAppService privateEmailAppService, IUserAppService userAppService)
        {
            this._privateEmailAppService = privateEmailAppService;
            this._userAppService = userAppService;
        }
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
            //return View("~/App/Main/views/home/home.cshtml");
            //return View();
        }

        public async Task<ActionResult> About()
        {
            //     var responseHtml = await Task.Factory.StartNew(() =>
            //_privateEmailAppService.Send2());
            Task task = null;
            Task.Run(() => { _privateEmailAppService.Send2(); });
            //await _privateEmailAppService.Send2();
            ViewBag.Message = "Your application description page.";
            var users = (await _userAppService.GetAllAsync
                (new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}