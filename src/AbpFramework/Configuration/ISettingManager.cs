using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 管理setting的接口
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// 获取当前setting.
        /// 获取设置值
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <returns>当前setting</returns>
        Task<string> GetSettingValueAsync(string name);
        /// <summary>
        /// 获取应用程序级别的当前设置值。
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <returns>当前setting</returns>
        Task<string> GetSettingValueForApplicationAsync(string name);
        /// <summary>
        /// 获取应用程序级别的当前设置值
        /// 如果fallbackToDefault为false，它只是从应用程序获取值，如果应用程序尚未为该设置定义值，则返回null。
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>当前setting值</returns>
        Task<string> GetSettingValueForApplicationAsync(string name, bool fallbackToDefault);
        /// <summary>
        /// 获取租户级别的设置的当前值。
        /// 由给定的租户重写。
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="tenantId">Tenant id</param>
        /// <returns>当前setting值</returns>
        Task<string> GetSettingValueForTenantAsync(string name, int tenantId);
        /// <summary>
        /// 获取租户级别的设置的当前值
        /// 如果fallbackToDefault为false，它只是从租户获取值，如果应用程序尚未为该设置定义值，则返回null。
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>当前setting值</returns>
        Task<string> GetSettingValueForTenantAsync(string name, int tenantId, bool fallbackToDefault);
        /// <summary>
        /// 获取用户级别当前的设置的值
        /// 由给定的租户、用户重写
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="userId">User id</param>
        /// <returns>当前setting值</returns>
        Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId);

        /// <summary>
        /// 获取用户级别当前的设置的值.
        /// 如果fallbackToDefault为真 由给定的租户、用户重写.
        /// 如果fallbackToDefault为假, 它只是从用户获取值，如果用户没有为该设置定义值，则返回null.
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="userId">User id</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>当前setting值</returns>
        Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId, bool fallbackToDefault);
        //Task<string> GetSettingValueForUserAsync(string name, UserIdentifier user)
        /// <summary>
        /// 获取所有设置的当前值。
        /// 设置值，由应用程序，当前租户（如果存在）和当前用户（如果存在）重写。
        /// </summary>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync();
        /// <summary>
        /// 获取所有设置的当前值。
        /// 设置值，由应用程序，当前租户（如果存在）和当前用户（如果存在）重写。
        /// </summary>
        /// <param name="scopes">One or more scope to overwrite</param>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes);

    }
}
