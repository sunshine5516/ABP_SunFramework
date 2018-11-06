using Abp.Zero.Authorization.Roles;
using Microsoft.AspNet.Identity;
using AbpFramework.Domain.Services;
using AbpFramework.Runtime.Session;
using AbpFramework.Runtime.Caching;
namespace Abp.Zero.Authorization.Users
{
    //public abstract class AbpUserManager<TRole, TUser>
    //    : UserManager<TUser,long>, IDomainService
    //    where TRole: AbpRole<TUser>, new()
    //    where TUser: AbpUser<TUser>
    //{
    //    #region 声明实例
    //    public IAbpSession AbpSession { get; set; }
    //    public AbpUserStore<TRole, TUser> AbpStore { get; }
    //    private readonly ICacheManager _cacheManager;
    //    private readonly ISettingManager _settingManager;
    //    #endregion
    //    #region 构造函数
    //    public AbpUserManager(AbpUserStore<TRole, TUser> userStore,
    //        ICacheManager cacheManager, ISettingManager settingManager)
    //        : base(userStore)
    //    {
    //        AbpStore = userStore;
    //        _cacheManager = cacheManager;
    //        _settingManager = settingManager;
    //    }
    //    #endregion
    //    #region 方法

    //    #endregion
    //}
}
