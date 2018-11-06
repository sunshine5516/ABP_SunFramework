namespace Abp.Zero.Common.Authorization.Roles
{
    public class RolePermissionSetting: PermissionSetting
    {
        /// <summary>
        /// 角色ID.
        /// </summary>
        public virtual int RoleId { get; set; }
    }
}
