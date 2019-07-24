using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 用于持久化后台任务BackgroundJobInfo。可以实现这个接口将后台任务BackgroundJobInfo存储到数据库
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        #region 声明实例
        private readonly Dictionary<long, BackgroundJobInfo> _jobs;
        private long _lastId;
        #endregion
        #region 构造函数
        public InMemoryBackgroundJobStore()
        {
            _jobs = new Dictionary<long, BackgroundJobInfo>();
        }
        #endregion
        #region 方法
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            if(!_jobs.ContainsKey(jobInfo.Id))
            {
                return Task.FromResult(0);
            }
            _jobs.Remove(jobInfo.Id);
            return Task.FromResult(0);
        }

        public Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            return Task.FromResult(_jobs[jobId]);
        }

        public Task<List<BackgroundJobInfo>> GetWaitJobsAsync(int maxResultCount)
        {
            var waitingJobs = _jobs.Values
                .Where(t => !t.IsAbandoned && t.NextTryTime <= DateTime.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();
            return Task.FromResult(waitingJobs);
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.Id = Interlocked.Increment(ref _lastId);
            _jobs[jobInfo.Id] = jobInfo;
            return Task.FromResult(0);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo.IsAbandoned)
            {
                return DeleteAsync(jobInfo);
            }

            return Task.FromResult(0);
        }
        #endregion
    }
}
