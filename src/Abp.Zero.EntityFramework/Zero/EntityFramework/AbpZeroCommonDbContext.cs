using Abp.EntityFramework.EntityFramework;
using Abp.Zero.Authorization.Roles;
using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Auditing;
using Abp.Zero.Common.Authorization;
using Abp.Zero.Common.Authorization.Roles;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Notifications;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
namespace Abp.Zero.EntityFramework
{
    public abstract class AbpZeroCommonDbContext<TRole, TUser> : AbpDbContext
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual IDbSet<TRole> Roles { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual IDbSet<TUser> Users { get; set; }
        /// <summary>
        /// 用户登录信息
        /// </summary>
        public virtual IDbSet<UserLogin> UserLogins { get; set; }
        /// <summary>
        /// 用户角色.
        /// </summary>
        public virtual IDbSet<UserRole> UserRoles { get; set; }
        /// <summary>
        /// 审计日志
        /// </summary>
        public virtual IDbSet<AuditLog> AuditLogs { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public virtual IDbSet<UserPermissionSetting> UserPermissions { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public virtual IDbSet<PermissionSetting> Permissions { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public virtual IDbSet<RolePermissionSetting> RolePermissions { get; set; }

        public virtual IDbSet<UserClaim> UserClaims { get; set; }
        /// <summary>
        /// User login attempts.
        /// </summary>
        public virtual IDbSet<UserLoginAttempt> UserLoginAttempts { get; set; }
        /// <summary>
        /// Notifications.
        /// </summary>
        public virtual IDbSet<NotificationInfo> Notifications { get; set; }

        /// <summary>
        /// Tenant notifications.
        /// </summary>
        public virtual IDbSet<TenantNotificationInfo> TenantNotifications { get; set; }

        /// <summary>
        /// User notifications.
        /// </summary>
        public virtual IDbSet<UserNotificationInfo> UserNotifications { get; set; }

        /// <summary>
        /// Notification subscriptions.
        /// </summary>
        public virtual IDbSet<NotificationSubscriptionInfo> NotificationSubscriptions { get; set; }
        /// <summary>
        /// 默认构造函数
        /// Do not directly instantiate this class. Instead, use dependency injection!
        /// </summary>
        protected AbpZeroCommonDbContext()
        {

        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="nameOrConnectionString">连接字符串</param>
        protected AbpZeroCommonDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected AbpZeroCommonDbContext(DbCompiledModel model)
            : base(model)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected AbpZeroCommonDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected AbpZeroCommonDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {

        }

        protected AbpZeroCommonDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AbpZeroCommonDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }
}
