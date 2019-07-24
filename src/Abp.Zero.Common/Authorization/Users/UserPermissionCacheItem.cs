using System;
using System.Collections.Generic;
namespace Abp.Zero.Common.Authorization.Users
{
    /// <summary>
    /// 用于缓存用户的角色和权限
    /// </summary>
    [Serializable]
    public class UserPermissionCacheItem
    {
        public const string CacheStoreName = "AbpZeroUserPermissions";
        public long UserId { get; set; }
        public List<int> RoleIds { get; set; }
        public HashSet<string> GrantedPermissions { get; set; }

        public HashSet<string> ProhibitedPermissions { get; set; }
        public UserPermissionCacheItem()
        {
            RoleIds = new List<int>();
            GrantedPermissions = new HashSet<string>();
            ProhibitedPermissions = new HashSet<string>();
        }

        public UserPermissionCacheItem(long userId)
            : this()
        {
            UserId = userId;
        }
    }
}
