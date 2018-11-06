using System;
namespace AbpFramework.Domain.Uow
{
    public class UnitOfWorkFailedEventArgs: EventArgs
    {
        /// <summary>
        /// 引发错误的异常
        /// </summary>
        public Exception Exception { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">Exception that caused failure</param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
