using AbpDemo.Application.MultiTenancy;
using AbpFramework.Application.Services.Dto;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AbpDemo.Web.Controllers
{
    public class TenantsController : ABPFrameworkDemoControllerBase
    {
        #region 声明实例
        private readonly ITenantAppService _tenantAppService;
        #endregion
        #region 构造函数
        public TenantsController(ITenantAppService tenantAppService)
        {
            this._tenantAppService = tenantAppService;
        }
        #endregion
        // GET: Tenants
        public async Task<ActionResult> Index()
        {
            var output = await _tenantAppService.GetAllAsync(new PagedResultRequestDto { MaxResultCount = int.MaxValue });
            return View(output);
        }
        public async Task<ActionResult> EditTenantModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.Get(new EntityDto(tenantId));
            return View("_EditTenantModal", tenantDto);
        }
    }
}