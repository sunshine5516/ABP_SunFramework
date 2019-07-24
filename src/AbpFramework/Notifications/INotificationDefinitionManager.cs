using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 定义根据name返回NotificationDefinition的一些方法
    /// </summary>
    public interface INotificationDefinitionManager
    {
        void Add(NotificationDefinition notificationDefinition);
        NotificationDefinition Get(string name);
        NotificationDefinition GetOrNull(string name);
        IReadOnlyList<NotificationDefinition> GetAll();
        /// <summary>
        /// 检测给定的名称的通知是否属于用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> IsAvailableAsync(string name, UserIdentifier user);
        // <summary>
        /// 获取用户所有的通知信息
        /// </summary>
        /// <param name="user">User.</param>
        Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(UserIdentifier user);
    }
}
