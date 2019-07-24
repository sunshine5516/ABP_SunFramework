using System;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 设置文件的作用域
    /// </summary>
    [Flags]
    public enum SettingScopes
    {
        /// <summary>
        /// 应用程序级别.
        /// </summary>
        Application = 1,

        /// <summary>
        /// 租户
        /// This is reserved
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// 用户
        /// </summary>
        User = 4,

        /// <summary>
        /// Represents a setting that can be configured/changed for all levels
        /// </summary>
        All = Application | Tenant | User
    }
}
