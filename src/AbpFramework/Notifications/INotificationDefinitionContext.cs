namespace AbpFramework.Notifications
{
    /// <summary>
    /// 在定义通知时用作上下文.
    /// </summary>
    public interface INotificationDefinitionContext
    {
        INotificationDefinitionManager Manager { get; }
    }
}
