using Abp.Zero.Authorization;
using Abp.Zero.Authorization.Roles;
using Abp.Zero.Common.Authorization.Users;
using Abp.Zero.Common.Zero.Configuration;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
namespace AbpDemo.Core.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public LogInManager(
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig, IRepository<Tenant> tenantRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            //AbpRoleManager<Role, User> roleManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
            :base(userManager
                 , unitOfWorkManager, multiTenancyConfig,
                 tenantRepository, userManagementConfig, iocResolver,
                  userLoginAttemptRepository)
        {

        }
    }
}
