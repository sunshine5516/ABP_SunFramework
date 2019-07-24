using AbpFramework.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于封装User和Notification关系的Entity.
    /// </summary>
    [Serializable]
    public class UserNotification : EntityDto<Guid>, IUserIdentifier
    {
        public int? TenantId { get; set; }

        public long UserId { get; set; }
        /// <summary>
        /// 当前通知状态.
        /// </summary>
        public UserNotificationState State { get; set; }
        /// <summary>
        /// The notification.
        /// </summary>
        public TenantNotification Notification { get; set; }
    }
}
