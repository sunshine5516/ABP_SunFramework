using Abp.Web.Mvc.Web.Mvc.Authorization;
using AbpDemo.Application.Roles;
using AbpDemo.Core.Authorization;
using AbpDemo.Web.Models.Roles;
using AbpFramework.Application.Services.Dto;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AbpDemo.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : ABPFrameworkDemoControllerBase
    {
        #region 声明实例
        private readonly IRoleAppService _roleAppService;
        #endregion
        #region 构造函数
        public RolesController(IRoleAppService roleAppService)
        {
            this._roleAppService = roleAppService;
        }
        #endregion
        #region 方法

        #endregion
        // GET: Roles
        public async Task<ActionResult> Index()
        {
            var roles=(await _roleAppService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items;
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            var model=new RoleListViewModel
            {
                Roles = roles,
                Permissions = permissions
            };
            return View(model);
        }
        public async Task<ActionResult> EditRoleModal(int roleId)
        {
            var role = await _roleAppService.Get(new EntityDto(roleId));
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            var model=new EditRoleModalViewModel
            {
                Role = role,
                Permissions = permissions
            };
            return View("_EditRoleModal", model);
        }
    }
}