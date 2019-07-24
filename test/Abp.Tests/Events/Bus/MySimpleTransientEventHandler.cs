using AbpFramework.Events.Bus.Handlers;
using System;
namespace Abp.Tests.Events.Bus
{
    public class MySimpleTransientEventHandler : IEventHandler<MySimpleEventData>, IDisposable
    {
        public static int HandleCount { get; set; }
        public static int DisposeCount { get; set; }
        public void Dispose()
        {
            ++DisposeCount;
        }

        public void HandleEvent(MySimpleEventData eventData)
        {
            var t = ++HandleCount;

        }
    }
}
