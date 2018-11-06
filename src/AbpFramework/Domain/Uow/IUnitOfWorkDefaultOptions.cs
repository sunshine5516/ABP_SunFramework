using System;
using System.Collections.Generic;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 默认UOW选项接口
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 作用域选项
        /// </summary>
        TransactionScopeOption Scope { get; set; }
        /// <summary>
        /// 是否是事务性的
        /// 如果未提供，则使用默认值
        /// </summary>
        bool IsTransactional { get; set; }
        bool IsTransactionScopeAvailable { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        TimeSpan? Timeout { get; set; }


        /// <summary>
        /// 如果这个UOW是事务性的，这个选项指出了事务的隔离级别。
        /// 默认true
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// A list of selectors to determine conventional Unit Of Work classes.
        /// </summary>
        List<Func<Type, bool>> ConventionalUowSelectors { get; }

        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称.</param>
        /// <param name="isEnabledByDefault">是否默认可用.</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// 重写过滤器配置
        /// </summary>
        /// <param name="filterName">过滤器名称.</param>
        /// <param name="isEnabledByDefault">是否默认可用.</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}
