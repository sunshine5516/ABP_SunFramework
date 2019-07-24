using AbpDemo.Application.Emailing.Dto;
using AbpDemo.Core;
using AbpFramework;
using AbpFramework.BackgroundJobs;
using AbpFramework.Net.Mail;
using AbpFramework.Net.Mail.Smtp;
using AbpFramework.Notifications;
using AbpFramework.UI;
using System.Threading.Tasks;

namespace AbpDemo.Application.Emailing
{
    public class PrivateEmailAppService : ABPMultiMVCAppServiceBase,IPrivateEmailAppService
    {
        #region 声明实例
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IEmailSender _smtpEmailSender;
        #endregion
        #region 构造函数
        public PrivateEmailAppService(IBackgroundJobManager backgroundJobManager, INotificationPublisher notificationPublisher,
            IEmailSender smtpEmailSender)
        {
            _backgroundJobManager = backgroundJobManager;
            _notificationPublisher = notificationPublisher;
            _smtpEmailSender = smtpEmailSender;
        }
        #endregion
        #region 方法

        public string Send2()
        {
            var targetUser = UserManager.FindByNameAsync("wp");
            if (targetUser == null)
            {
                throw new UserFriendlyException("There is no such a user: " +"wp");
            }
            //var currentUser = GetCurrentTenantAsync();
             _backgroundJobManager.EnqueueAsync<SendPrivateEmailJob, SendPrivateEmailJobArgs>(
                new SendPrivateEmailJobArgs
                {
                    Subject ="Hello",
                    Body ="Hello world",
                    //SenderUserId = currentUser.Id,
                    SenderUserId = 35,
                    TargetTenantId = AbpSession.TenantId,
                    TargetUserId = targetUser.Id
                });
            if (true)
            {
                var notificationData = new MessageNotificationData(
                    string.Format("{0} sent you an email with subject: {1}",
                        "Java",
                        "hello java world"
                        )
                    );
                long Id = 34;
                UserIdentifier userIdentifier = new UserIdentifier(0, 34);
                _notificationPublisher.PublishAsync(NotificationNames.YouHaveAnEmail, notificationData, userIds: new[] { userIdentifier });
            }
            return "";
        }


        public async Task Send(SendPrivateEmailInput input)
        {
            var targetUser = await UserManager.FindByNameAsync(input.UserName);
            if (targetUser==null)
            {
                throw new UserFriendlyException("There is no such a user: " + input.UserName);
            }
            var currentUser =await GetCurrentUserAsync();
            await _backgroundJobManager.EnqueueAsync<SendPrivateEmailJob, SendPrivateEmailJobArgs>(
                new SendPrivateEmailJobArgs
                {
                    Subject = input.Subject,
                    Body = input.Body,
                    SenderUserId = currentUser.Id,
                    //SenderUserId = 35,
                    TargetTenantId = AbpSession.TenantId,
                    TargetUserId = targetUser.Id
                });
            if(input.SendNotification)
            {
                var notificationData = new MessageNotificationData(
                    string.Format("{0} sent you an email with subject: {1}",
                        "Java",
                        input.Subject
                        )
                    );
                UserIdentifier userIdentifier = new UserIdentifier(0, targetUser.Id);
                await _notificationPublisher.PublishAsync(NotificationNames.YouHaveAnEmail, notificationData, userIds: new[] { userIdentifier });
            }
            string message = "You hava been assigned one task into your todo list.";
            await _smtpEmailSender.SendAsync("961128502@qq.com", "18761430298@163.com", "New Todo item", message);
        }
        public static class NotificationNames
        {
            public const string YouHaveAnEmail = "App.YouHaveAnEmail";
        }
        #endregion

    }
}
