using AbpFramework.Dependency;
using Castle.Core;
using System.Threading;
namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 默认<see cref="ICurrentUnitOfWorkProvider"/>实现. 
    /// </summary>
    public class AsyncLocalCurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider, ITransientDependency
    {
        #region 声明实例
        /// <inheritdoc />
        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(); }
            set { SetCurrentUow(value); }
        }

        private static readonly AsyncLocal<LocalUowWrapper> AsyncLocalUow = new AsyncLocal<LocalUowWrapper>();

        #endregion
        #region 构造函数
        public AsyncLocalCurrentUnitOfWorkProvider()
        { }
        #endregion
        #region 方法       
        private static IUnitOfWork GetCurrentUow()
        {
            var uow = AsyncLocalUow.Value?.UnitOfWork;
            if (uow == null)
            {
                return null;
            }
            if (uow.IsDisposed)
            {
                AsyncLocalUow.Value = null;
                return null;
            }
            return uow;
        }
        private static void SetCurrentUow(IUnitOfWork value)
        {
            lock (AsyncLocalUow)
            {
                if (value == null)//如果在set的时候设置为null，便表示要退出当前工作单元
                {
                    if (AsyncLocalUow.Value == null)
                    {
                        return;
                    }

                    if (AsyncLocalUow.Value.UnitOfWork?.Outer == null)
                    {
                        AsyncLocalUow.Value.UnitOfWork = null;
                        AsyncLocalUow.Value = null;
                        return;
                    }

                    AsyncLocalUow.Value.UnitOfWork = AsyncLocalUow.Value.UnitOfWork.Outer;
                }
                else
                {
                    if (AsyncLocalUow.Value?.UnitOfWork == null)
                    {
                        if (AsyncLocalUow.Value != null)
                        {
                            AsyncLocalUow.Value.UnitOfWork = value;
                        }

                        AsyncLocalUow.Value = new LocalUowWrapper(value);
                        return;
                    }

                    value.Outer = AsyncLocalUow.Value.UnitOfWork;
                    AsyncLocalUow.Value.UnitOfWork = value;
                }
            }
        }

        private class LocalUowWrapper
        {
            public IUnitOfWork UnitOfWork { get; set; }

            public LocalUowWrapper(IUnitOfWork unitOfWork)
            {
                UnitOfWork = unitOfWork;
            }
        }
        #endregion

    }
}
