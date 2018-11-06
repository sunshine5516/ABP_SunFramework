using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// UOW管理接口
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前活动的工作单元（如果不存在则返回null）。
        /// </summary>
        IActiveUnitOfWork Current { get; }
        /// <summary>
        /// 开始一个新的工作单元。
        /// </summary>
        /// <returns>A handle to be able to complete the unit of work</returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        /// 开始一个新的工作单元.
        /// </summary>
        /// <returns>A handle to be able to complete the unit of work</returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        /// 开始一个新的工作单元.
        /// </summary>
        /// <returns>A handle to be able to complete the unit of work</returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
