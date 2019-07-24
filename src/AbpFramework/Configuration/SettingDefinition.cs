using System;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 定义一个setting.
    /// 设置文件用于配置和更改应用程序的行为。
    /// </summary>
    public class SettingDefinition
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get;private set; }
        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
        //public ILocalizableString DisplayName { get; set; }
        /// <summary>
        /// 此设置的简要说明。
        /// </summary>
        //public ILocalizableString Description { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 范围
        /// Default value: <see cref="SettingScopes.Application"/>.
        /// </summary>
        public SettingScopes Scopes { get; set; }
        public bool IsInherited { get; set; }
        public SettingDefinitionGroup Group { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 用于存储与此设置相关的自定义对象。
        /// </summary>
        public object CustomData { get; set; }
        /// <summary>
        /// 客户是否可见
        /// 某些设置对客户可见（例如电子邮件服务器密码）可能很危险。
        /// 默认值: false.
        /// </summary>
        public bool IsVisibleToClients { get; set; }
        public SettingDefinition(
            string name,
            string defaultValue,
            string displayName = null,
            SettingDefinitionGroup group = null,
            string description = null,
            SettingScopes scopes = SettingScopes.Application,
            bool isVisibleToClients = false,
            bool isInherited = true,
            object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            DefaultValue = defaultValue;
            DisplayName = displayName;
            Group = @group;
            Description = description;
            Scopes = scopes;
            IsVisibleToClients = isVisibleToClients;
            IsInherited = isInherited;
            CustomData = customData;
        }
    }
}
