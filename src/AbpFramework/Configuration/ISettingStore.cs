using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 接口定义了相关方法用于从数据源读取和更改setting值
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// 获取设置值
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="name">设置名称</param>
        /// <returns>Setting实例</returns>
        Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name);
        /// <summary>
        /// 删除设置值
        /// </summary>
        /// <param name="setting">Setting实例</param>
        Task DeleteAsync(SettingInfo setting);
        /// <summary>
        /// 添加设置.
        /// </summary>
        /// <param name="setting">Setting to add</param>
        Task CreateAsync(SettingInfo setting);
        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="setting">Setting to add</param>
        Task UpdateAsync(SettingInfo setting);
        /// <summary>
        /// 获取设置集合
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>设置集合</returns>
        Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId);

    }
}
