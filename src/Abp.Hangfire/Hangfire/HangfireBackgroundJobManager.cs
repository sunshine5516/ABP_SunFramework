using Abp.Hangfire.Hangfire.Configuration;
using AbpFramework.BackgroundJobs;
using AbpFramework.Threading.BackgroundWorkers;
using Hangfire;
using System;
using System.Threading.Tasks;
using HangfireBackgroundJob = Hangfire.BackgroundJob;
namespace Abp.Hangfire.Hangfire
{
    public class HangfireBackgroundJobManager : BackgroundWorkerBase, IBackgroundJobManager
    {
        #region 声明实例
        private readonly IBackgroundJobConfiguration _backgroundJobConfiguration;
        private readonly IAbpHangfireConfiguration _hangfireConfiguration;
        #endregion
        #region 构造函数
        public HangfireBackgroundJobManager(
            IBackgroundJobConfiguration backgroundJobConfiguration,
            IAbpHangfireConfiguration hangfireConfiguration)
        {
            _backgroundJobConfiguration = backgroundJobConfiguration;
            _hangfireConfiguration = hangfireConfiguration;
        }
        #endregion
        #region 方法
        public override void Start()
        {
            base.Start();
            if(_hangfireConfiguration.Server==null&&_backgroundJobConfiguration.IsJobExecutionEnabled)
            {
                _hangfireConfiguration.Server = new BackgroundJobServer();
            }
        }
        public override void WaitToStop()
        {
            if (_hangfireConfiguration.Server != null)
            {
                try
                {
                    _hangfireConfiguration.Server.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }
            base.WaitToStop();
        }
        #endregion
        public Task<bool> DeleteAsync(string jobId)
        {
            if(string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(nameof(jobId));
            }
            bool successfulDeletion = HangfireBackgroundJob.Delete(jobId);
            return Task.FromResult(successfulDeletion);
        }

        public Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, 
            BackgroundJobPriority priority = BackgroundJobPriority.Normal, 
            TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
        {
            string jobUniqueIdentifier = string.Empty;
            if(!delay.HasValue)
            {
                jobUniqueIdentifier = HangfireBackgroundJob.Enqueue<TJob>(job => job.Execute(args));
            }
            else
            {
                jobUniqueIdentifier = HangfireBackgroundJob.Schedule<TJob>(job => job.Execute(args), delay.Value);
            }
            return Task.FromResult(jobUniqueIdentifier);
        }
    }
}
