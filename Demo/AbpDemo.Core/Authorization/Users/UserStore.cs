using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization.Users;
using AbpDemo.Core.Authorization.Roles;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
namespace AbpDemo.Core.Authorization.Users
{
    public class UserStore: AbpUserStore<Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimStore
            ):base(
                userRepository,userLoginRepository,
                userRoleRepository,roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,userClaimStore
                )
        {

        }
    }
}
