using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 提供持久化NotificationInfo的方法
    /// </summary>
    public interface INotificationStore
    {
        /// <summary>
        /// 添加通知订阅
        /// </summary>
        /// <param name="subscription"></param>
        Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription);
        /// <summary>
        /// 删除用户订阅通知
        /// </summary>
        Task DeleteSubscriptionAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId);
        /// <summary>
        /// 添加通知
        /// </summary>
        Task InsertNotificationAsync(NotificationInfo notification);
        /// <summary>
        /// 根据ID获取通知信息
        /// </summary>
        Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId);
        /// <summary>
        /// 添加一个用户通知
        /// </summary>
        Task InsertUserNotificationAsync(UserNotificationInfo userNotification);
        /// <summary>
        /// 获取通知的订阅
        /// </summary>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName, string entityId);
        /// <summary>
        /// 获取特定租户通知的订阅
        /// </summary>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName,
            string entityTypeName, string entityId);
        /// <summary>
        /// 获取用户的所有订阅
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(UserIdentifier user);
        /// <summary>
        /// 检测某个用户是否订阅了某个通知
        /// </summary>
        /// <returns></returns>
        Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId);
        /// <summary>
        /// 更新用户订阅状态
        /// </summary>
        Task UpdateUserNotificationStateAsync(int? tenantId, Guid userNotificationId, UserNotificationState state);
        /// <summary>
        /// 更新用户的所有订阅状态
        /// </summary>
        Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state);
        /// <summary>
        /// 删除用户的通知
        /// </summary>
        Task DeleteUserNotificationAsync(int? tenantId, Guid userNotificationId);
        /// <summary>
        /// 删除用户的所有通知.
        /// </summary>
        Task DeleteAllUserNotificationsAsync(UserIdentifier user);
        /// <summary>
        /// 获取用户通知
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(UserIdentifier user,
            UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue);
        /// <summary>
        /// 获取用户通知的数量
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null);
        /// <summary>
        /// 获取用户通知
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">Skip count.</param>
        Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(int? tenantId, Guid userNotificationId);

        /// <summary>
        /// 添加租户通知
        /// </summary>
        Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="notification">The notification.</param>
        Task DeleteNotificationAsync(NotificationInfo notification);
    }
}
