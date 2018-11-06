namespace Abp.Zero.Common.Authorization.Users
{
    public class UserPermissionSetting: PermissionSetting
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long UserId { get; set; }
    }
}
