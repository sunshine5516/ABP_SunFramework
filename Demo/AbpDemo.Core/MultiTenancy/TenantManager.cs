using Abp.Zero.Common.MultiTenancy;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.Editions;
using AbpFramework.Configuration;
using AbpFramework.Domain.Repositories;
namespace AbpDemo.Core.MultiTenancy
{
    public class TenantManager: AbpTenantManager<Tenant, User>
    {
        private readonly ISettingManager _settingManager;
        public TenantManager(IRepository<Tenant> tenantRepository, EditionManager editionManager
            , ISettingManager settingManager)
            :base(tenantRepository,editionManager)
        {
            this._settingManager = settingManager;
        }
    }
}
