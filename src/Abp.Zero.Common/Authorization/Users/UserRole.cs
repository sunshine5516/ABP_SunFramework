using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Authorization.Users
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    [Table("AbpUserRoles")]
    public class UserRole : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual long UserId { get; set; }
        public virtual int RoleId { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRole()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        public UserRole(int? tenantId, long userId, int roleId)
        {
            TenantId = tenantId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
