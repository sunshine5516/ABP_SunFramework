using AbpDemo.Application.MultiTenancy.Dto;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.Editions;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Repositories;
namespace AbpDemo.Application.MultiTenancy
{
    public class TenantAppService: AsyncCrudAppService<Tenant, TenantDto,int,
        PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        #region 声明实例
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userMamager;
        #endregion
        #region 构造函数
        public TenantAppService(IRepository<Tenant, int> repository,
            TenantManager tenantManager, EditionManager editionManager,
            RoleManager roleManager, UserManager userMamager)
            :base(repository)
        {
            this._tenantManager = tenantManager;
            this._editionManager = editionManager;
            this._roleManager = roleManager;
            this._userMamager = userMamager;
        }
        #endregion
        #region 方法

        #endregion
    }
}
