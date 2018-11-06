using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 这个接口不能被注入或直接使用。使用 <see cref="IUnitOfWorkManager"/> 实现.
    /// 定义了UOW同步和异步的complete方法。实现UOW完成时候的逻辑。
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        ///  保存所有，提交事务
        /// </summary>
        void Complete();
        /// <summary>
        ///  异步保存所有，提交事务
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();
    }
}
