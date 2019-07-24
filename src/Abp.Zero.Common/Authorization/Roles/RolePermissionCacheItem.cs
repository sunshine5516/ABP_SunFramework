using System;
using System.Collections.Generic;
namespace Abp.Zero.Common.Authorization.Roles
{
    [Serializable]
    public class RolePermissionCacheItem
    {
        public const string CacheStoreName = "AbpZeroRolePermissions";
        public long RoleId { get; set; }
        public HashSet<string> GrantedPermissions { get; set; }
        public RolePermissionCacheItem()
        {
            GrantedPermissions = new HashSet<string>();
        }
        public RolePermissionCacheItem(int roleId)
           : this()
        {
            RoleId = roleId;
        }
    }
}
