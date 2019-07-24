using Abp.Zero.Authorization.Users;
using AbpDemo.Core.Authorization.Roles;
using AbpFramework.Authorization;
using AbpFramework.Configuration;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Caching;
namespace AbpDemo.Core.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(UserStore userStore,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            ISettingManager settingManager,
            IPermissionManager permissionManager)
            :base(userStore,unitOfWorkManager, roleManager,
                 cacheManager,settingManager, permissionManager)
        { }
    }
    //public class UserManager
    //{
    //}
}
