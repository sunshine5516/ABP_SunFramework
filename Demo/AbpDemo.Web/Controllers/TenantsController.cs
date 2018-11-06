using AbpDemo.Application.MultiTenancy;
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
        public ActionResult Index()
        {
            return View();
        }
    }
}