namespace AbpFramework.Events.Bus
{
    /// <summary>
    /// 此接口必须由具有单个泛型参数的事件数据类实现，并且此参数将由继承使用。 
    /// 如果你的evendata继承了这个接口。就可以按照继承层次往上逐个触发事件
    /// For example;
    /// Assume that Student inherits From Person. When trigger an EntityCreatedEventData{Student},
    /// EntityCreatedEventData{Person} is also triggered if EntityCreatedEventData implements
    /// this interface.
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        object[] GetConstructorArgs();
    }
}
