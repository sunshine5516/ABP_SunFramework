using AbpFramework.Domain.Entities;
using AbpFramework.Events.Bus.Entities;
using AbpFramework.Events.Bus.Handlers;
using Shouldly;
using Xunit;
namespace Abp.Tests.Events.Bus
{
    
    public class EventBus_EntityEvents_Test : EventBusTestBase
    {
        [Fact]
        public void Should_Call_Created_And_Changed_Once()
        {
            var handler = new MyEventHandler();
            EventBus.Register<EntityChangedEventData<MyEntity>>(handler);
            EventBus.Register<EntityCreatedEventData<MyEntity>>(handler);
            EventBus.Trigger(new EntityCreatedEventData<MyEntity>(new MyEntity()));
            EventBus.Trigger(new EntityChangedEventData<MyEntity>(new MyEntity()));
            handler.EntityCreatedEventCount.ShouldBe(1);
            handler.EntityChangedEventCount.ShouldBe(2);
        }
    }
    public class MyEntity : Entity
    {

    }
    public class MyEventHandler :
        IEventHandler<EntityChangedEventData<MyEntity>>,
        IEventHandler<EntityCreatedEventData<MyEntity>>
    {
        public int EntityChangedEventCount { get; set; }
        public int EntityCreatedEventCount { get; set; }
        public void HandleEvent(EntityChangedEventData<MyEntity> eventData)
        {
            EntityChangedEventCount++;
        }

        public void HandleEvent(EntityCreatedEventData<MyEntity> eventData)
        {
            EntityCreatedEventCount++;
        }
    }
}
