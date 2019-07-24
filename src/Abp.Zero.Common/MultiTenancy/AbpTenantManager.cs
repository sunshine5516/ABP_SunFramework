using Abp.MultiTenancy;
using Abp.Zero.Common.Application.Editions;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Services;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Caching;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Abp.Zero.Common.MultiTenancy
{
    /// <summary>
    /// 租户管理.
    /// 实现域逻辑 for <see cref="AbpTenant{TUser}"/>.
    /// </summary>
    /// <typeparam name="TTenant">租户的类型</typeparam>
    /// <typeparam name="TUser">用户的类型</typeparam>
    public class AbpTenantManager<TTenant,TUser>:IDomainService
        //IEventHandler<EntityChangedEventData<TTenant>>,
        //IEventHandler<EntityDeletedEventData<Edition>>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUserBase
    {
        #region 声明实例
        public AbpEditionManager EditionManager { get; set; }       
        public ICacheManager CacheManager { get; set; }
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }
        protected IRepository<TTenant> TenantRepository { get; set; }
        //protected IRepository<TenantFeatureSetting, long> TenantFeatureRepository { get; set; };
        //private readonly IAbpZeroFeatureValueStore _featureValueStore;
        //public IFeatureManager FeatureManager { get; set; }
        //public ILocalizationManager LocalizationManager { get; set; }
        #endregion
        #region 构造函数
        public AbpTenantManager(IRepository<TTenant> tenantRepository,            
            AbpEditionManager editionManager)
        {
            this.TenantRepository = tenantRepository;
            this.EditionManager = editionManager;
        }
        #endregion
        #region 方法
        public virtual IQueryable<TTenant> Tenants { get { return TenantRepository.GetAll(); } }
        public virtual async Task CreateAsync(TTenant tenant)
        {
            await ValidateTenantAsync(tenant);
            if(await TenantRepository.FirstOrDefaultAsync(t=>t.TenancyName==tenant.TenancyName)!=null)
            {
                //throw new UserFriendlyException(string.Format(L("TenancyNameIsAlreadyTaken"), tenant.TenancyName));
                throw new Exception("租户名称已存在");
            }
            await TenantRepository.InsertAsync(tenant);
        }
        public async Task UpdateAsync(TTenant tenant)
        {
            if (await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenant.TenancyName && t.Id != tenant.Id) != null)
            {
                //throw new UserFriendlyException(string.Format(L("TenancyNameIsAlreadyTaken"), tenant.TenancyName));
                throw new Exception("租户名称已存在");
            }

            await TenantRepository.UpdateAsync(tenant);
        }
        public virtual async Task<TTenant> GetByIdAsync(int id)
        {
            var tenant = await FindByIdAsync(id);
            if (tenant == null)
            {
                throw new AbpException("There is no tenant with id: " + id);
            }
            return tenant;
        }
        public virtual async Task<TTenant> FindByIdAsync(int id)
        {
            return await TenantRepository.FirstOrDefaultAsync(id);
        }
        public virtual Task<TTenant> FindByTenancyNameAsync(string tenancyName)
        {
            return TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenancyName);
        }
        public virtual async Task DeleteAsync(TTenant tenant)
        {
            await TenantRepository.DeleteAsync(tenant);
        }
        protected virtual async Task ValidateTenantAsync(TTenant tenant)
        {
            await ValidateTenancyNameAsync(tenant.TenancyName);
        }
        protected virtual Task ValidateTenancyNameAsync(string tenancyName)
        {
            if(!Regex.IsMatch(tenancyName,AbpTenant<TUser>.TenancyNameRegex))
            {
                throw new Exception("非法租户名称");
                //throw new UserFriendlyException(L("InvalidTenancyName"));
            }
            return Task.FromResult(0);
        }
        #endregion
    }
}
