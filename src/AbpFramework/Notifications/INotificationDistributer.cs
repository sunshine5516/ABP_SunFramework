using AbpFramework.Domain.Services;
using System;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 向用户分发通知接口，也就是建立Notification和User的关系.
    /// </summary>
    public interface INotificationDistributer:IDomainService
    {
        /// <summary>
        /// 向用户分发给定的通知。
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        Task DistributeAsync(Guid notificationId);
    }
}
