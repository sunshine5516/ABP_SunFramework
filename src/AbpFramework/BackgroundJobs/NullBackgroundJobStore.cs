using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// <see cref="IBackgroundJobStore"/>空实现.
    /// <see cref="IBackgroundJobStore"/>未实际持久性存储实现的以及
    /// 未启动<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>，使用该类
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            return Task.FromResult(new BackgroundJobInfo());
        }

        public Task<List<BackgroundJobInfo>> GetWaitJobsAsync(int maxResultCount)
        {
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}
