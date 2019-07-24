using Abp.Zero.Common.Authorization.Roles;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Runtime.Caching;
namespace Abp.Zero.Common.Runtime.Caching
{
    public static class AbpZeroCacheManagerExtensions
    {
        public static ITypedCache<string, RolePermissionCacheItem> GetRolePermissionCache
            (this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, RolePermissionCacheItem>(RolePermissionCacheItem.CacheStoreName);
        }
        public static ITypedCache<string, UserPermissionCacheItem> GetUserPermissionCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, UserPermissionCacheItem>
                (UserPermissionCacheItem.CacheStoreName);
        }
    }
}
