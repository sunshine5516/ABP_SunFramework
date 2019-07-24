using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 定义设置定义管理器。
    /// 主要完成注册到ABP中的SettingDefinition初始化
    /// </summary>
    internal class SettingDefinitionManager : ISettingDefinitionManager, ISingletonDependency
    {
        #region 声明实例
        private readonly IocManager _icoManager;
        private readonly ISettingsConfiguration _settingsConfiguration;
        private readonly IDictionary<string, SettingDefinition> _settings;
        #endregion
        #region 构造函数
        public SettingDefinitionManager(IocManager iocManager, 
            ISettingsConfiguration settingsConfiguration)
        {
            this._icoManager = iocManager;
            _settingsConfiguration = settingsConfiguration;
            _settings = new Dictionary<string, SettingDefinition>();
        }
        #endregion
        #region 方法
        public void Initialize()
        {
            var context = new SettingDefinitionProviderContext(this);
            foreach(var providerType in _settingsConfiguration.Providers)
            {
                using (var provider = CreateProvider(providerType))
                {
                    foreach (var settings in provider.Object.GetSettingDefinitions(context))
                    {
                        _settings[settings.Name] = settings;
                    }
                }
            }
        }
        public IReadOnlyList<SettingDefinition> GetAllSettingDefinitions()
        {
            return _settings.Values.ToImmutableList();
        }

        public SettingDefinition GetSettingDefinition(string name)
        {
            SettingDefinition settingDefinition;
            if(!_settings.TryGetValue(name,out settingDefinition))
            {
                throw new AbpException("There is no setting defined with name: " + name);
            }
            return settingDefinition;
        }
        private IDisposableDependencyObjectWrapper<SettingProvider> CreateProvider(Type providerType)
        {
            return _icoManager.ResolveAsDisposable<SettingProvider>(providerType);
        }
        #endregion

    }
}
