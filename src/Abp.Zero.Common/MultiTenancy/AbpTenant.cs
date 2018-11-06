using Abp.Zero.Common.Application.Editions;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Domain.Entities.Auditing;
namespace Abp.MultiTenancy
{
    public abstract class AbpTenant<TUser> : AbpTenantBase, IFullAudited<TUser>
        where TUser : AbpUserBase
    {
        #region 声明实例
        /// <summary>
        /// Current <see cref="Edition"/> of the Tenant.
        /// </summary>
        public virtual Edition Edition { get; set; }
        public virtual int? EditionId { get; set; }
        public TUser CreatorUser { get; set; }
        public TUser LastModifierUser { get; set; }
        public TUser DeleterUser { get; set; }
        #endregion


        #region 构造函数
        public AbpTenant()
        {
            IsActive = true;
        }
        protected AbpTenant(string tenancyName, string name)
            : this()
        {
            TenancyName = tenancyName;
            Name = name;
        }
        #endregion        
    }
}
