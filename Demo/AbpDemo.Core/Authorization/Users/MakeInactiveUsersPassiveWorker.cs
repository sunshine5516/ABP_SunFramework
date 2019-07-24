using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
using AbpFramework.Threading.BackgroundWorkers;
using AbpFramework.Threading.Timers;
using System;
namespace AbpDemo.Core.Authorization.Users
{
    /// <summary>
    /// 后台工作者，如果用户30天没登录，则标为不活跃
    /// </summary>
    public class MakeInactiveUsersPassiveWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        #region 声明实例
        private readonly IRepository<User, long> _userRepository;
        #endregion
        #region 构造函数
        public MakeInactiveUsersPassiveWorker(AbpTimer timer, IRepository<User, long> userRepository)
            : base(timer)
        {
            _userRepository = userRepository;
            Timer.Period = 5000; //5 seconds (good for tests, but normally will be more)
        }
        #endregion
        #region 方法
        [UnitOfWork]
        protected override void DoWork()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var oneMonthAgo = DateTime.Now.Subtract(TimeSpan.FromDays(30));
                var inactiveUsers = _userRepository.GetAllList(u =>
                  u.IsActive && ((u.LastLoginTime < oneMonthAgo && u.LastLoginTime != null)
                  || (u.CreationTime < oneMonthAgo && u.LastLoginTime == null)));
                foreach(var inactiveUser in inactiveUsers)
                {
                    inactiveUser.IsActive = false;
                    Logger.Info(inactiveUser + " made passive since he/she did not login in last 30 days.");
                }
                CurrentUnitOfWork.SaveChanges();
            }
        }
        #endregion

    }
}
