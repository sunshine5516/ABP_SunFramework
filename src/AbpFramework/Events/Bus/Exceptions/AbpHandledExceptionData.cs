using System;
namespace AbpFramework.Events.Bus.Exceptions
{
    public class AbpHandledExceptionData:ExceptionData
    {
        public AbpHandledExceptionData(Exception exception)
            :base(exception)
        {

        }
    }
}
