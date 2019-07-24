using AbpDemo.Application.Emailing;
using AbpDemo.Application.Tasks;
using AbpDemo.Application.Users;
using AbpDemo.Web.Models.Users;
using AbpFramework.Application.Services.Dto;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AbpDemo.Web.Controllers
{
    public class UsersController : ABPFrameworkDemoControllerBase
    {
        #region 声明实例
        private readonly IUserAppService _userAppService;
        private readonly ITaskService _taskService;
        private readonly IPrivateEmailAppService _privateEmailAppService;
        #endregion
        #region 构造函数
        public UsersController(IUserAppService userAppService,
            ITaskService taskService, IPrivateEmailAppService privateEmailAppService)
        {
            this._userAppService = userAppService;
            this._taskService = taskService;
            this._privateEmailAppService = privateEmailAppService;
        }
        #endregion
        #region Action
        // GET: Users
        public async Task<ActionResult> Index()
        {
            //await _privateEmailAppService.Send2();
            //_taskService.Send();
            _taskService.TestBackgroundJobs();
            var users = (await _userAppService.GetAllAsync
                (new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles=roles
            };
            return View(model);
        }
        public async Task<ActionResult> EditUserModal(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles= (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
        #endregion

    }
}