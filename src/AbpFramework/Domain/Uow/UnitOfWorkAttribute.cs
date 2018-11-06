using System;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 用于标注某个方法位UnitOfWork的Attribute类. 通过这个特性，可以指定是否启用UOW，事务隔离级别，TransactionScopeOption等
    /// 这个属性用来表示声明的方法是原子的，应该被认为是一个工作单元。
    /// 具有此属性的方法被拦截，打开数据库连接并在调用方法之前启动事务。
    /// 在方法调用结束时，如果没有异常，则提交事务并将所有更改应用于数据库，否则将回滚。
    /// </summary>
    /// <remarks>
    /// 如果在调用此方法之前已经有一个工作单元，则此属性不起作用，如果这样，则使用相同的事务。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkAttribute:Attribute
    {
        /// <summary>
        /// 作用域选项.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }
        /// <summary>
        /// 是否是事务性的
        /// 如果未提供，则使用默认值
        public bool? IsTransactional { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan? Timeout { get; set; }
        /// <summary>
        /// 如果这个UOW是事务性的，这个选项指出了事务的隔离级别。
        /// 如果未提供，则使用默认值
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
        /// <summary>
        /// 用于防止为该方法启动一个工作单元。
        /// 如果已经有一个开始的工作单元，则忽略这个属性。
        /// Default: false.
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="isTransactional">
        /// 是否事务性的?
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timeout">超时时间</param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isTransactional">是否事务性的?</param>
        /// <param name="timeout">超时时间</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 构造函数
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isolationLevel">事物隔离级别</param>
        /// <param name="timeout">事务超时为毫秒</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">作用域选项</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">作用域选项</param>
        /// <param name="isTransactional">
        /// Is this unit of work will be transactional?
        /// </param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional)
        {
            Scope = scope;
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">作用域选项</param>
        /// <param name="timeout">超时时间</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}
