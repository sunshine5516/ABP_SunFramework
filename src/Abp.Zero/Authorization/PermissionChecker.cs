using Abp.Zero.Authorization.Roles;
using Abp.Zero.Authorization.Users;
using AbpFramework;
using AbpFramework.Authorization;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Session;
using Castle.Core.Logging;
using System.Threading.Tasks;

namespace Abp.Zero.Authorization
{
    public abstract class PermissionChecker<TRole, TUser> : IPermissionChecker,
        ITransientDependency//, IIocManagerAccessor
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        #region 声明实例
        private readonly AbpUserManager<TRole, TUser> _userManager;
        public IIocManager IocManager { get; set; }
        public ILogger Logger { get; set; }
        public IAbpSession AbpSession { get; set; }
        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }
        #endregion
        #region 构造函数
        protected PermissionChecker(AbpUserManager<TRole, TUser> userManager)
        {
            _userManager = userManager;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }
        #endregion
        #region 方法

        #endregion
        public virtual async Task<bool> IsGrantedAsync(string permissionName)
        {
            return AbpSession.UserId.HasValue&&
                await _userManager.IsGrantedAsync(AbpSession.UserId.Value, permissionName);
        }
        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await _userManager.IsGrantedAsync(userId, permissionName);
        }
        [UnitOfWork]
        public virtual async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            if(CurrentUnitOfWorkProvider==null||CurrentUnitOfWorkProvider.Current==null)
            {
                return await IsGrantedAsync(user.UserId, permissionName);
            }
            using (CurrentUnitOfWorkProvider.Current.SetTenantId(user.TenantId))
            {
                return await _userManager.IsGrantedAsync(user.UserId, permissionName);
            }
        }
    }
}
