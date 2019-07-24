using System;
namespace AbpFramework.Events.Bus.Exceptions
{
    /// <summary>
    /// 异常信息
    /// </summary>
    public class ExceptionData:EventData
    {
        public Exception Exception { get; private set; }
        public ExceptionData(Exception exception)
        {
            Exception = exception;
        }
    }
}
