using AbpFramework.Logging;
using AbpFramework.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// ISettingStore.默认实现
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        #region 实例
        public static DefaultConfigSettingStore Instance { get; } = new DefaultConfigSettingStore();
        #endregion
        #region 构造函数
        private DefaultConfigSettingStore()
        {

        }
        #endregion
        #region 方法

        
        public Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
            return AbpTaskCache.CompletedTask;
        }

        public Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
            return AbpTaskCache.CompletedTask;
        }

        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }

        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];
            if(value==null)
            {
                return Task.FromResult<SettingInfo>(null);
            }
            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));
        }

        public Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
            return AbpTaskCache.CompletedTask;
        }
        #endregion
    }
}
