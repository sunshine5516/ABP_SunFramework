using AbpFramework.Runtime.Caching;
using System.Collections.Generic;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// <see cref="ICacheManager"/>的扩展.
    /// </summary>
    public static class CacheManagerSettingExtensions
    {
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetApplicationSettingsCache
            (this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, Dictionary<string, SettingInfo>>
                (AbpCacheNames.ApplicationSettings);
        }
        public static ITypedCache<int, Dictionary<string, SettingInfo>> GetTenantSettingsCache
            (this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, Dictionary<string, SettingInfo>>
                (AbpCacheNames.TenantSettings);
        }
        /// <summary>
        /// Gets user settings cache.
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetUserSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(AbpCacheNames.UserSettings);
        }
      
    }
}
