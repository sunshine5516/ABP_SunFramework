using System;
using System.Collections.Generic;
namespace AbpFramework.Auditing
{
    /// <summary>
    /// 配置审计信息
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// 是否可用
        /// 默认：True
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// 如果为True,则允许未登录的用户保存审计日志。
        /// Default: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }
        /// <summary>
        /// 忽略审计日志记录的序列化类型。
        /// </summary>
        List<Type> IgnoredTypes { get; }
        /// <summary>
        /// 选择器列表，用于选择应作为默认值审计的类/接口。
        /// </summary>
        IAuditingSelectorList Selectors { get; }
    }
}
