using AbpFramework.Extensions;
using AbpFramework.Threading;
using System.Threading.Tasks;
namespace AbpFramework.Configuration
{
    public static class SettingManagerExtensions
    {
        public static string GetSettingValue(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync(name));
        }
        public static T GetSettingValue<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync<T>(name));
        }
        public static async Task<T> GetSettingValueAsync<T>(this ISettingManager settingManager, string name)
           where T : struct
        {
            return (await settingManager.GetSettingValueAsync(name)).To<T>();
        }
        public static T GetSettingValueForApplication<T>(this ISettingManager settingManager, string name)
          where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.
            GetSettingValueForApplicationAsync<T>(name));
        }
        public static async Task<T> GetSettingValueForApplicationAsync<T>(this ISettingManager settingManager, string name)
          where T : struct
        {
            return (await settingManager.GetSettingValueForApplicationAsync(name)).To<T>();
        }
        public static T GetSettingValueForTenant<T>(this ISettingManager settingManager, string name, int tenantId)
          where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForTenantAsync<T>(name, tenantId));
        }
        public static async Task<T> GetSettingValueForTenantAsync<T>(this ISettingManager settingManager, string name, int tenantId)
          where T : struct
        {
            return (await settingManager.GetSettingValueForTenantAsync(name, tenantId)).To<T>();
        }
        /// <summary>
        /// 获取用户级别的设置的当前值.
        /// 它获取设置值，由给定的租户和用户覆盖。
        /// </summary>
        /// <typeparam name="T">Type of the setting to get</typeparam>
        /// <param name="settingManager">Setting manager</param>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="userId">User id</param>
        /// <returns>Current value of the setting for the user</returns>
        public static T GetSettingValueForUser<T>(this ISettingManager settingManager, string name, int? tenantId, long userId)
            where T : struct
        {
            return AsyncHelper.RunSync(()=>settingManager.GetSettingValueForUserAsync<T>(name, tenantId, userId));
        }
        public static async Task<T> GetSettingValueForUserAsync<T>(this ISettingManager settingManager, string name, int? tenantId, long userId)
          where T : struct
        {
            return (await settingManager.GetSettingValueForUserAsync(name, tenantId, userId)).To<T>();
        }
    }
}
