using Shouldly;
using System;
using Xunit;

namespace Abp.Tests.Events.Bus
{
    public class EventBus_Exception_Test:EventBusTestBase
    {
        [Fact]
        public void Should_Throw_Single_Exception_If_Only_One_Of_Handlers_Fails()
        {
            //Assert.Throws<InvalidOperationException>(() => operation());
            //Assert.Equal(3, Math.Max(3, 2));
            EventBus.Register<MySimpleEventData>(
                eventData =>
                {
                    //new MySimpleEventData(2);
                    throw new Exception("This exception is intentionally thrown!");
                });
            var appException = Assert.Throws<Exception>(() =>
              {
                  EventBus.Trigger<MySimpleEventData>(null, new MySimpleEventData(1));
              });
            appException.Message.ShouldBe("This exception is intentionally thrown!");
        }
        void operation()
        {
            throw new InvalidOperationException();
        }
    }
}
