using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 该接口由ABP内部使用。
    /// 定义了外层的IUnitOfWork的引用和UOW的begin方法
    /// </summary>
    public interface IUnitOfWork: IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        string Id { get;}
        IUnitOfWork Outer { get; set; }
        void Begin(UnitOfWorkOptions options);
    }
}
