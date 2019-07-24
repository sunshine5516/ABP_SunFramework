using AbpFramework.Dependency;
using AbpFramework.Json;
using AbpFramework.Threading;
using AbpFramework.Threading.BackgroundWorkers;
using AbpFramework.Threading.Timers;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 作业管理，继承自IBackgroundJobManager.
    /// </summary>
    public class BackgroundJobManager : PeriodicBackgroundWorkerBase,
        IBackgroundJobManager, ISingletonDependency
    {
        #region 声明实例
        public static int JobPollPeriod { get; set; }
        private readonly IIocResolver _iocResolver;
        private readonly IBackgroundJobStore _store;
        #endregion
        #region 构造函数
        static BackgroundJobManager()
        {
            JobPollPeriod = 5000;
        }
        public BackgroundJobManager(
           IIocResolver iocResolver,
           IBackgroundJobStore store,
           AbpTimer timer)
           : base(timer)
        {
            _store = store;
            _iocResolver = iocResolver;
            Timer.Period = JobPollPeriod;
        }
        #endregion
        #region 方法
        public async Task<bool> DeleteAsync(string jobId)
        {
            if (long.TryParse(jobId, out long finalJobId) == false)
            {
                throw new ArgumentException($"The jobId '{jobId}' should be a number.", nameof(jobId));
            }
            BackgroundJobInfo jobInfo = await _store.GetAsync(finalJobId);
            if (jobInfo == null)
            {
                return false;
            }
            await _store.DeleteAsync(jobInfo);
            return true;
        }

        public async Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority
            = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };
            if (delay.HasValue)
            {
                jobInfo.NextTryTime = DateTime.Now.Add(delay.Value);
            }
            await _store.InsertAsync(jobInfo);
            return jobInfo.Id.ToJsonString();
        }

        protected override void DoWork()
        {
            var waitingJobs = AsyncHelper.RunSync(() => _store.GetWaitJobsAsync(1000));
            foreach (var job in waitingJobs)
            {
                TryProcessJob(job);
            }
        }

        private void TryProcessJob(BackgroundJobInfo jobInfo)
        {
            try
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = DateTime.Now;
                var jobType = Type.GetType(jobInfo.JobType);
                using (var job = _iocResolver.ResolveAsDisposable(jobType))
                {
                    try
                    {
                        var jobExecuteMethod = job.Object.GetType().GetTypeInfo().GetMethod("Execute");
                        var argsType = jobExecuteMethod.GetParameters()[0].ParameterType;
                        var argsObj = JsonConvert.DeserializeObject(jobInfo.JobArgs, argsType);
                        jobExecuteMethod.Invoke(job.Object, new[] { argsObj });

                        AsyncHelper.RunSync(() => _store.DeleteAsync(jobInfo));
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message, ex);
                        var nextTryTime = jobInfo.CalculateNextTryTime();
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        TryUpdate(jobInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);

                jobInfo.IsAbandoned = true;

                TryUpdate(jobInfo);
            }

        }
        private void TryUpdate(BackgroundJobInfo jobInfo)
        {
            try
            {
                _store.UpdateAsync(jobInfo);
            }catch (Exception updateEx)
            {
                Logger.Warn(updateEx.ToString(), updateEx);
            }
        }
        #endregion
    }
}
