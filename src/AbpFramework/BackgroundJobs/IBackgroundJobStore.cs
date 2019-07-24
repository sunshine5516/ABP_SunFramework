using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 用于持久化后台任务BackgroundJobInfo，定义存储/获取后台作业的接口。
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// 根据ID获取BackgroundJobInfo
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<BackgroundJobInfo> GetAsync(long jobId);
        Task InsertAsync(BackgroundJobInfo jobInfo);
        /// <summary>
        /// 获取等待中的任务
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<BackgroundJobInfo>> GetWaitJobsAsync(int maxResultCount);
        Task DeleteAsync(BackgroundJobInfo jobInfo);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}
