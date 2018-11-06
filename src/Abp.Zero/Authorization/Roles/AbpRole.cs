using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization.Roles;
using AbpFramework.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;
using System;
namespace Abp.Zero.Authorization.Roles
{
    /// <summary>
    /// 角色.
    /// </summary>
    /// <remarks> 
    /// 应用程序应使用权限来检查是否授予用户执行操作的权限。
    /// 在角色为静态之前，无法检查“用户是否有角色”（<see cref ="AbpRoleBase.IsStatic"/>）。
    /// 静态角色可以在代码中使用，不能被用户删除。
    /// 用户可以添加/删除非静态（动态）角色，编码时我们无法知道他们的名字。
    /// 用户可以拥有多个角色。 因此，用户将拥有所有已分配角色的所有权限。
    /// </remarks>
    public abstract class AbpRole<TUser> : AbpRoleBase, IRole<int>, IFullAudited<TUser>
        where TUser : AbpUser<TUser>
    {
        public TUser CreatorUser { get; set; }
        public TUser LastModifierUser { get; set; }
        public TUser DeleterUser { get; set; }
        protected AbpRole()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="displayName">Display name of the role</param>
        protected AbpRole(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="name">Unique role name</param>
        /// <param name="displayName">Display name of the role</param>
        protected AbpRole(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }
    }
}
