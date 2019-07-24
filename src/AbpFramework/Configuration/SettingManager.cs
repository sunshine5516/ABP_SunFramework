using AbpFramework.Dependency;
using AbpFramework.Runtime.Caching;
using AbpFramework.Runtime.Session;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AbpFramework.Collections.Extensions;
using System.Linq;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 实现<see cref="ISettingManager"/>接口，管理数据库中的设置值.
    /// </summary>
    public class SettingManager : ISettingManager, ISingletonDependency
    {
        #region 声明实例
        public const string ApplicationSettingsCacheKey = "ApplicationSettings";
        public IAbpSession AbpSession { get; set; }
        public ISettingStore SettingStore { get; set; }
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly ITypedCache<string, Dictionary<string, SettingInfo>> _applicationSettingCache;
        private readonly ITypedCache<int, Dictionary<string, SettingInfo>> _tenantSettingCache;
        private readonly ITypedCache<string, Dictionary<string, SettingInfo>> _userSettingCache;

        #endregion
        #region 构造函数
        public SettingManager(ISettingDefinitionManager settingDefinitionManager, ICacheManager cacheManager)
        {
            _settingDefinitionManager = settingDefinitionManager;

            AbpSession = NullAbpSession.Instance;
            SettingStore = DefaultConfigSettingStore.Instance;

            _applicationSettingCache = cacheManager.GetApplicationSettingsCache();
            _tenantSettingCache = cacheManager.GetTenantSettingsCache();
            _userSettingCache = cacheManager.GetUserSettingsCache();
        }
        #endregion
        #region Public方法
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync()
        {
            return await GetAllSettingValuesAsync(SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User);
        }

        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes)
        {
            var settingDefinitions = new Dictionary<string, SettingDefinition>();
            var settingValues = new Dictionary<string, ISettingValue>();
            foreach(var setting in _settingDefinitionManager.GetAllSettingDefinitions())
            {
                settingDefinitions[setting.Name] = setting;
                settingValues[setting.Name] = new SettingValueObject(setting.Name, setting.DefaultValue);
            }
            if (scopes.HasFlag(SettingScopes.Application))
            {
                foreach(var settingValue in await GetAllSettingValuesForApplicationAsync())
                {
                    var setting= settingDefinitions.GetOrDefault(settingValue.Name);
                    if (setting == null || !setting.Scopes.HasFlag(SettingScopes.Application))
                    {
                        continue;
                    }

                    if (!setting.IsInherited &&
                        ((setting.Scopes.HasFlag(SettingScopes.Tenant) && AbpSession.TenantId.HasValue) || (setting.Scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue)))
                    {
                        continue;
                    }

                    settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                }
            }
            //if (scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue)
            //{
            //    foreach(var settingValue in await GetAllSettingValuesForUserAsync(AbpSession.ToUserIdentifier()))
            //    {
            //        var setting = settingDefinitions.GetOrDefault(settingValue.name);
            //        if (setting != null && setting.Scopes.HasFlag(SettingScopes.User))
            //        {
            //            settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
            //        }
            //    }
            //}
            if (scopes.HasFlag(SettingScopes.Tenant) && AbpSession.TenantId.HasValue)
            {
                foreach (var settingValue in await GetAllSettingValuesForTenantAsync(AbpSession.TenantId.Value))
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);

                    //TODO: Conditions get complicated, try to simplify it
                    if (setting == null || !setting.Scopes.HasFlag(SettingScopes.Tenant))
                    {
                        continue;
                    }

                    if (!setting.IsInherited &&
                        (setting.Scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue))
                    {
                        continue;
                    }

                    settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                }
            }

            return settingValues.Values.ToImmutableList();
        }
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync()
        {
            return (await GetApplicationSettingsAsync()).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForTenantAsync(int tenantId)
        {
            return (await GetReadOnlyTenantSettings(tenantId)).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }
        public Task<string> GetSettingValueAsync(string name)
        {
            return GetSettingValueInternalAsync(name, AbpSession.TenantId, AbpSession.UserId);
        }

        public Task<string> GetSettingValueForApplicationAsync(string name)
        {
            return GetSettingValueInternalAsync(name);
        }

        public Task<string> GetSettingValueForApplicationAsync(string name, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name, fallbackToDefault: fallbackToDefault);
        }

        public Task<string> GetSettingValueForTenantAsync(string name, int tenantId)
        {
            return GetSettingValueInternalAsync(name, tenantId);
        }

        public Task<string> GetSettingValueForTenantAsync(string name, int tenantId, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name, tenantId, fallbackToDefault: fallbackToDefault);
        }

        public Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId)
        {
            return GetSettingValueInternalAsync(name, tenantId, userId);
        }

        public Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name, tenantId, userId, fallbackToDefault);
        }
        #endregion
        #region 私有方法
        private async Task<string> GetSettingValueInternalAsync(string name, int? tenantId = null, 
            long? userId = null, bool fallbackToDefault = true)
        {
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);
            //if (settingDefinition.Scopes.HasFlag(SettingScopes.User) && userId.HasValue)
            //{
            //    var settingValue=await GetSettingValueForUserOrNullAsync(new UserIdentifier(tenantId, userId.Value), name);
            //    if(settingValue!=null)
            //    {
            //        return settingValue.Value;
            //    }
            //    if(!fallbackToDefault)
            //    {
            //        return null;
            //    }
            //    if(!settingDefinition.IsInherited)
            //    {
            //        return settingDefinition.DefaultValue;
            //    }
            //}
            //Get for tenant if defined
            if (settingDefinition.Scopes.HasFlag(SettingScopes.Tenant) && tenantId.HasValue)
            {
                var settingValue = await GetSettingValueForTenantOrNullAsync(tenantId.Value, name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }
                if (!fallbackToDefault)
                {
                    return null;
                }

                if (!settingDefinition.IsInherited)
                {
                    return settingDefinition.DefaultValue;
                }
            }
            //Get for application if defined
            if (settingDefinition.Scopes.HasFlag(SettingScopes.Application))
            {
                var settingValue = await GetSettingValueForApplicationOrNullAsync(name);
                if(settingValue !=null)
                {
                    return settingValue.Value;
                }
                if(!fallbackToDefault)
                {
                    return null;
                }
            }
            return settingDefinition.DefaultValue;
        }
        private async Task<SettingInfo> GetSettingValueForTenantOrNullAsync(int tenantId, string name)
        {
            return (await GetReadOnlyTenantSettings(tenantId)).GetOrDefault(name);
        }

        private async Task<ImmutableDictionary<string, SettingInfo>> GetReadOnlyTenantSettings(int tenantId)
        {
            var cachedDictionary = await GetTenantSettingsFromCache(tenantId);
            lock(cachedDictionary)
            {
                return cachedDictionary.ToImmutableDictionary();
            }
        }

        private async Task<Dictionary<string, SettingInfo>> GetTenantSettingsFromCache(int tenantId)
        {
            return await _tenantSettingCache.GetAsync(
                tenantId,
                async () =>
                {
                    var dictionary = new Dictionary<string, SettingInfo>();
                    var settingValues = await SettingStore.GetAllListAsync(tenantId, null);
                    foreach(var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }
                    return dictionary;
                });
        }
        private async Task<SettingInfo> GetSettingValueForApplicationOrNullAsync(string name)
        {
            return (await GetApplicationSettingsAsync()).GetOrDefault(name);
        }

        private async Task<Dictionary<string, SettingInfo>> GetApplicationSettingsAsync()
        {
            return await _applicationSettingCache.GetAsync(ApplicationSettingsCacheKey,
                async () =>
                {
                    var dictionary = new Dictionary<string, SettingInfo>();
                    var settingValues = await SettingStore.GetAllListAsync(null, null);
                    foreach(var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }
                    return dictionary;
                });
        }
        #endregion
        #region Nested classes
        private class SettingValueObject : ISettingValue
        {
            public string Name { get; private set; }

            public string Value { get; private set; }
            public SettingValueObject(string name,string value)
            {
                Name = name;
                Value = value;
            }
        }
        #endregion
    }
}
