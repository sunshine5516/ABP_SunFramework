using Abp.MultiTenancy;
using Abp.Zero.Authorization.Roles;
using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization;
using Abp.Zero.Common.Authorization.Users;
using Abp.Zero.Common.Zero.Configuration;
using Abp.Zero.IdentityFramework;
using AbpFramework;
using AbpFramework.Auditing;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
using AbpFramework.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
namespace Abp.Zero.Authorization
{
    public abstract class AbpLogInManager<TTenant, TRole, TUser> : ITransientDependency
        where TTenant : AbpTenant<TUser>
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        #region 声明实例
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
        protected IMultiTenancyConfig MultiTenancyConfig { get; }
        protected IRepository<TTenant> TenantRepository { get; }
        protected AbpUserManager<TRole, TUser> UserManager { get; }
        protected IUserManagementConfig UserManagementConfig { get; }
        protected IIocResolver IocResolver { get; }
        //protected AbpRoleManager<TRole, TUser> RoleManager { get; }
        public IClientInfoProvider ClientInfoProvider { get; set; }
        protected IRepository<UserLoginAttempt, long> UserLoginAttemptRepository { get; }
        #endregion
        #region 构造函数
        protected AbpLogInManager(AbpUserManager<TRole, TUser> userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig, IRepository<TTenant> tenantRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            //AbpRoleManager<TRole, TUser> roleManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
        {
            UnitOfWorkManager = unitOfWorkManager;
            MultiTenancyConfig = multiTenancyConfig;
            TenantRepository = tenantRepository;
            UserManager = userManager;
            UserManagementConfig = userManagementConfig;
            //RoleManager = roleManager;
            UserLoginAttemptRepository = userLoginAttemptRepository;
        }
        #endregion
        #region 方法
        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="userNameOrEmailAddress">用户名或电子邮件</param>
        /// <param name="plainPassword">密码</param>
        /// <param name="tenancyName">租户名称</param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<AbpLoginResult<TTenant, TUser>> LoginAsync(string userNameOrEmailAddress,
            string plainPassword, string tenancyName = null, bool shouldLockout = true)
        {
            var result = await LoginAsyncInternal(userNameOrEmailAddress, plainPassword, tenancyName
                , shouldLockout);
            await SaveLoginAttempt(result, tenancyName, userNameOrEmailAddress);//保存登录记录
            return result;
        }
        [UnitOfWork]
        public virtual async Task<AbpLoginResult<TTenant, TUser>> LoginAsync
            (UserLoginInfo login, string tenancyName = null)
        {
            var result = await LoginAsyncInternal(login, tenancyName);
            await SaveLoginAttempt(result, tenancyName, login.ProviderKey + "@" + login.LoginProvider);
            return result;
        }
        //[UnitOfWork]
        //public virtual void tempttt()
        //{
        //    UnitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
        //}
        /// <summary>
        /// 保存登录记录
        /// </summary>
        /// <param name="loginResult"></param>
        /// <param name="tenancyName"></param>
        /// <param name="userNameOrEmailAddress"></param>
        /// <returns></returns>
        protected virtual async Task SaveLoginAttempt(AbpLoginResult<TTenant, TUser> loginResult
            , string tenancyName, string userNameOrEmailAddress)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                var tenantId = loginResult.Tenant != null ?
                    loginResult.Tenant.Id : (int?)null;
                using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    var loginAttempt = new UserLoginAttempt
                    {
                        TenantId = tenantId,
                        TenancyName = tenancyName,
                        UserId = loginResult.User != null ? loginResult.User.Id : (long?)null,
                        UserNameOrEmailAddress = userNameOrEmailAddress,
                        Result = loginResult.Result,
                        BrowserInfo = ClientInfoProvider.BrowserInfo,
                        ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                        ClientName = ClientInfoProvider.ComputerName,
                    };
                    await UserLoginAttemptRepository.InsertAsync(loginAttempt);
                    await UnitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }
        }
        /// <summary>
        /// 内部登录
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="tenancyName"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        protected virtual async Task<AbpLoginResult<TTenant, TUser>> LoginAsyncInternal(
            string userNameOrEmailAddress, string plainPassword, string tenancyName,
            bool shouldLockout)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }
            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }
            TTenant tenant = null;
            using (UnitOfWorkManager.Current.SetTenantId(null))
            {
                if (!MultiTenancyConfig.IsEnabled)
                {
                    tenant = await GetDefaultTenantAsync();
                }
                else if (!string.IsNullOrWhiteSpace(tenancyName))
                {
                    tenant = await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenancyName);
                    if (tenant == null)
                    {
                        return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.InvalidTenancyName);
                    }
                    if (!tenant.IsActive)
                    {
                        return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.TenantIsNotActive, tenant);
                    }
                }
            }
            var tenantId = tenant == null ? (int?)null : tenant.Id;
            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources
                    (userNameOrEmailAddress, plainPassword, tenant);
                var user = await UserManager.AbpStore.FindByNameOrEmailAsync(tenantId,
                    userNameOrEmailAddress);
                if (user == null)
                {
                    return new AbpLoginResult<TTenant, TUser>
                        (AbpLoginResultType.InvalidUserNameOrEmailAddress, tenant);
                }
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    return new AbpLoginResult<TTenant, TUser>
                        (AbpLoginResultType.LockedOut, tenant, user);
                }
                if (!loggedInFromExternalSource)
                {
                    UserManager.InitializeLockoutSettings(tenantId);
                    var verificationResult = UserManager.PasswordHasher.VerifyHashedPassword
                        (user.Password, plainPassword);//验证密码是否符合 hashedPassword
                    if (verificationResult == PasswordVerificationResult.Failed)
                    {
                        return await GetFailedPasswordValidationAsLoginResultAsync(user, tenant, shouldLockout);
                    }
                    if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
                    {
                        return await GetSuccessRehashNeededAsLoginResultAsync(user, tenant);
                    }
                    await UserManager.ResetAccessFailedCountAsync(user.Id);
                }
                return await CreateLoginResultAsync(user, tenant);
            }

        }
        protected virtual async Task<AbpLoginResult<TTenant, TUser>> LoginAsyncInternal
            (UserLoginInfo login, string tenancyName)
        {
            if (login == null || login.LoginProvider.IsNullOrEmpty() || login.ProviderKey.IsNullOrEmpty())
            {
                throw new ArgumentException("login");
            }
            //Get and check tenant
            TTenant tenant = null;
            if (!MultiTenancyConfig.IsEnabled)
            {
                tenant = await GetDefaultTenantAsync();
            }
            else if (!string.IsNullOrWhiteSpace(tenancyName))
            {
                tenant = await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName
                  == tenancyName);
                if (tenant == null)
                {
                    return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.InvalidTenancyName);
                }
                if (!tenant.IsActive)
                {
                    return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.TenantIsNotActive, tenant);
                }
            }
            int? tenantId = tenant == null ? (int?)null : tenant.Id;
            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var user = await UserManager.AbpStore.FindAsync(tenantId, login);
                if (user == null)
                {
                    return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.UnknownExternalLogin, tenant);
                }
                return await CreateLoginResultAsync(user, tenant);
            }
        }
        protected virtual async Task<bool> TryLoginFromExternalAuthenticationSources
            (string userNameOrEmailAddress, string plainPassword, TTenant tenant)
        {
            if (!UserManagementConfig.ExternalAuthenticationSources.Any())
            {
                return false;
            }
            foreach (var sourceType in UserManagementConfig.ExternalAuthenticationSources)
            {
                using (var source = IocResolver.ResolveAsDisposable<IExternalAuthenticationSource<TTenant, TUser>>(sourceType))
                {
                    if (await source.Object.TryAuthenticateAsync(userNameOrEmailAddress, plainPassword, tenant))
                    {
                        var tenantId = tenant == null ? (int?)null : tenant.Id;
                        using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                        {
                            var user = await UserManager.AbpStore.FindByNameOrEmailAsync(tenantId,
                                userNameOrEmailAddress);
                            if (user == null)
                            {
                                user = await source.Object.CreateUserAsync(userNameOrEmailAddress, tenant);
                                user.TenantId = tenantId;
                                user.AuthenticationSource = source.Object.Name;
                                user.Password = UserManager.PasswordHasher.HashPassword
                                    (Guid.NewGuid().ToString("N").Left(16));
                                if (user.Roles == null)
                                {
                                    user.Roles = new List<UserRole>();
                                    //foreach (var defaultRole in RoleManager.Roles.Where(r => r.TenantId == tenantId && r.IsDefault).ToList())
                                    //{
                                    //    user.Roles.Add(new UserRole(tenantId, user.Id, defaultRole.Id));
                                    //}
                                }
                                await UserManager.AbpStore.CreateAsync(user);
                            }
                            else
                            {
                                await source.Object.UpdateUserAsync(user, tenant);
                                user.AuthenticationSource = source.Object.Name;
                                await UserManager.AbpStore.UpdateAsync(user);
                            }
                            await UnitOfWorkManager.Current.SaveChangesAsync();
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        protected virtual async Task<AbpLoginResult<TTenant, TUser>> CreateLoginResultAsync
            (TUser user, TTenant tenant = null)
        {
            if (!user.IsActive)
            {
                return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.UserIsNotActive);
            }
            if (await IsEmailConfirmationRequiredForLoginAsync(user.TenantId)
                && !user.IsEmailConfirmed)
            {
                return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.UserPhoneNumberIsNotConfirmed);
            }
            user.LastLoginTime = DateTime.Now;
            await UserManager.AbpStore.UpdateAsync(user);//更新用户信息
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new AbpLoginResult<TTenant, TUser>(tenant, user,
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
            //UserManager.CreateIdentityAsync创建表示用户的身份声明
        }
        protected virtual async Task<TTenant> GetDefaultTenantAsync()
        {
            var tenant = await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName ==
              AbpTenant<TUser>.DefaultTenantName);
            if (tenant == null)
            {
                throw new AbpException("There should be a 'Default' tenant if multi-tenancy is disabled!");
            }
            return tenant;
        }
        protected virtual async Task<AbpLoginResult<TTenant, TUser>> GetFailedPasswordValidationAsLoginResultAsync
            (TUser user, TTenant tenant = null, bool shouldLockout = false)
        {
            if (shouldLockout)
            {
                if (await TryLockOutAsync(user.TenantId, user.Id))
                {
                    return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.LockedOut
                        , tenant, user);
                }
            }
            return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.InvalidPassword,
                tenant, user);
        }
        protected virtual async Task<bool> TryLockOutAsync(int? tenantId, long userId)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    (await UserManager.AccessFailedAsync(userId)).CheckErrors();
                    var isLockOut = await UserManager.IsLockedOutAsync(userId);
                    await UnitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                    return isLockOut;
                }
            }
        }
        protected virtual async Task<AbpLoginResult<TTenant, TUser>> GetSuccessRehashNeededAsLoginResultAsync
            (TUser user, TTenant tenant = null, bool shouldLockout = false)
        {
            return await GetFailedPasswordValidationAsLoginResultAsync(user, tenant, shouldLockout);
        }
        /// <summary>
        /// 是否需要电子邮件确认
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        protected virtual async Task<bool> IsEmailConfirmationRequiredForLoginAsync(int? tenantId)
        {
            //return false;
            return await Task.FromResult(false);
            //if(tenantId.HasValue)
            //{
            //    return await SettingManager.GetSettingValueForTenantAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, tenantId.Value);
            //}
            //return await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
        }
        #endregion
    }
}
