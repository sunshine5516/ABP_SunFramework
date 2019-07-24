using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于存储用户通知
    /// </summary>
    [Serializable]
    [Table("AbpUserNotifications")]
    public class UserNotificationInfo : Entity<Guid>, IHasCreationTime, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long UserId { get; set; }
        public virtual Guid TenantNotificationId { get; set; }
        public virtual UserNotificationState State { get; set; }
        #region 构造函数
        public UserNotificationInfo()
        {

        }
        public UserNotificationInfo(Guid id)
        {
            Id = id;
            State = UserNotificationState.Unread;
            CreationTime = DateTime.Now;
        }
        #endregion
    }
}
