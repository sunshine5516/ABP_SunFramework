using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 封装了UnitOfWork参数的类
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// 作用域选项
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// 是否是事务性的
        /// 如果未提供，则使用默认值
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// 超时时间(毫秒)
        /// 如果未提供，则使用默认值
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 如果这个UOW是事务性的，这个选项指出了事务的隔离级别。
        /// 如果未提供，则使用默认值
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// if unit of work is used in an async scope.
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }
        /// <summary>
        /// 用来启用/禁用某些过滤器。
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }
        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }
            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }
            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }
        internal void FillOuterUowFiltersForNonProvidedOptions(List<DataFilterConfiguration> filterOverrides)
        {
            foreach (var filterOverride in filterOverrides)
            {
                if (FilterOverrides.Any(fo => fo.FilterName == filterOverride.FilterName))
                {
                    continue;
                }

                FilterOverrides.Add(filterOverride);
            }
        }
    }
}
