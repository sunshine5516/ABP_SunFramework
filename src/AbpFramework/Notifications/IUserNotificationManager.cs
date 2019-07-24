using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// user notifications管理，用于获取，删除UserNotification，以及更改UserNotification的状态。
    /// </summary>
    public interface IUserNotificationManager
    {
        /// <summary>
        /// 获取用户的通知。
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        Task<List<UserNotification>> GetUserNotificationsAsync(UserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue);
        /// <summary>
        /// 获取用户的通知数量
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null);
        /// <summary>
        /// 根据给定的ID获取用户通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userNotificationId"></param>
        /// <returns></returns>
        Task<UserNotification> GetUserNotificationAsync(int? tenantId, Guid userNotificationId);
        /// <summary>
        /// 更新用户通知状态
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userNotificationId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task UpdateUserNotificationStateAsync(int? tenantId, Guid userNotificationId, UserNotificationState state);
        /// <summary>
        /// 更新用户的所有通知状态
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">New state.</param>
        Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state);

        /// <summary>
        /// 删除用户通知
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="userNotificationId">The user notification id.</param>
        Task DeleteUserNotificationAsync(int? tenantId, Guid userNotificationId);

        /// <summary>
        /// 删除用户的所有通知.
        /// </summary>
        /// <param name="user">User.</param>
        Task DeleteAllUserNotificationsAsync(UserIdentifier user);
    }
}
