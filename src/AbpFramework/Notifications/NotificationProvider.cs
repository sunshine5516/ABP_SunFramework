using AbpFramework.Dependency;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 抽象基类，用于向<see cref="INotificationDefinitionManager"/>中添加<see cref="NotificationDefinition"/>
    /// </summary>
    public abstract class NotificationProvider : ITransientDependency
    {
        public abstract void SetNotifications(INotificationDefinitionContext context);
    }
}
