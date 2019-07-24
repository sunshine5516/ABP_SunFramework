using AbpFramework.Threading.BackgroundWorkers;
using System;
using System.Threading.Tasks;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 定义作业管理器的接口
    /// </summary>
    public interface IBackgroundJobManager: IBackgroundWorker
    {
        /// <summary>
        /// 作业排队
        /// </summary>
        /// <typeparam name="TJob">作业类型</typeparam>
        /// <typeparam name="TArgs">作业参数的类型.</typeparam>
        /// <param name="args">作业参数.</param>
        /// <param name="priority">作业优先权.</param>
        /// <param name="delay">工作延迟（第一次尝试之前的等待时间）.</param>
        /// <returns>后台作业的Id.</returns>
        Task<string> EnqueueAsync<TJob, TArgs>(TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>;
        /// <summary>
        /// 用指定的jobId删除一个作业。
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string jobId);
    }
}
