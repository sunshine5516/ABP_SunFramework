using Abp.Dapper.Dapper.Repositories;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core;
using AbpFramework;
using AbpFramework.BackgroundJobs;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using AbpFramework.Events.Bus;
using AbpFramework.Notifications;
using AbpFramework.UI;
using System;
using System.Threading.Tasks;

namespace AbpDemo.Application.Tasks
{
    public class TaskAssignedEventData : EventData
    {
        public User User { get; set; }
        public Task Task { get; set; }
        public TaskAssignedEventData(Task task, User user)
        {
            this.Task = task;
            this.User = user;
        }
    }
    public class TaskService : ABPMultiMVCAppServiceBase,ITaskService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<BackgroundJobInfo, long> _backgroundJobRepository;
        private readonly IDapperRepository<BackgroundJobInfo,long> _dapperRepository;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly INotificationPublisher _notificationPublisher;
        public TaskService(IBackgroundJobManager backgroundJobManager,
            IRepository<BackgroundJobInfo, long> backgroundJobRepository,
            IDapperRepository<BackgroundJobInfo, long> dapperRepository,
            INotificationSubscriptionManager notificationSubscriptionManager,
            INotificationPublisher notificationPublisher)
        {
            this._backgroundJobManager = backgroundJobManager;
            this._backgroundJobRepository = backgroundJobRepository;
            this._dapperRepository = dapperRepository;
            this._notificationSubscriptionManager = notificationSubscriptionManager;
            this._notificationPublisher = notificationPublisher;
        }
        /// <summary>
        /// 订阅一个一般通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task Subscribe_SentFrendshipRequest(int? tenantId, long userId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "SentFrendshipRequest"); ;
        }
        /// <summary>
        /// 订阅一个实体通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public async Task Subscribe_CommentPhoto(int? tenantId, long userId, Guid photoId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), 
                "CommentPhoto", new EntityIdentifier(typeof(Core.Authorization.Users.User), photoId));
        }

        public void Test()
        {
            var targetUser = UserManager.FindByNameAsync("admin");
            UserIdentifier userIdentifier = new UserIdentifier(0, 35);
            //    _notificationPublisher.PublishAsync("NewTask", new NotificationData(), null,
            //NotificationSeverity.Info, new[] { userIdentifier });
            var notificationData = new MessageNotificationData(
                        string.Format("{0} sent you an email with subject: {1}",
                            //currentUser.UserName,
                            "Jack",
                            "Jack,你好"
                            )
                        );
            //UserIdentifier userIdentifier = new UserIdentifier(0, 35);
            _notificationPublisher.PublishAsync("App.YouHaveAnEmail", notificationData, userIds: new[] { userIdentifier });
            _backgroundJobManager.EnqueueAsync<TestJob, int>(520);
            //await Send();
        }
        public  void Send()
        {
            var targetUser =  UserManager.FindByNameAsync("admin");
            UserIdentifier userIdentifier = new UserIdentifier(0, 34);
            if (targetUser == null)
            {
                throw new UserFriendlyException("There is no such a user: " + "admin");
            }

            var currentUser =  GetCurrentUserAsync();

             _backgroundJobManager.EnqueueAsync<SendPrivateEmailJob, SendPrivateEmailJobArgs>(
                new SendPrivateEmailJobArgs
                {
                    Subject = "后台任务TEST",
                    Body = "hello java world",
                    SenderUserId = currentUser.Id,
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

                 _notificationPublisher.PublishAsync("YouHaveAnEmail", notificationData, userIds: new[] { userIdentifier });
            }
        }
        public void TestBackgroundJobs()
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = "AbpDemo.Application.Tasks.TestJob, AbpDemo.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                JobArgs = "42",
                Priority = BackgroundJobPriority.High,
                TryCount = 0,
                NextTryTime = DateTime.Now.AddSeconds(1),
                IsAbandoned=false,
                CreationTime=DateTime.Now
            };
            var people = _dapperRepository.Query("select * from AbpBackgroundJobs");
            _backgroundJobRepository.InsertAsync(jobInfo);
        }
    }
}
