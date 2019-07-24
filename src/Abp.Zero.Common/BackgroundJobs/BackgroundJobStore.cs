using AbpFramework.BackgroundJobs;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Zero.Common.BackgroundJobs
{
    public class BackgroundJobStore : IBackgroundJobStore, ITransientDependency
    {
        #region 声明实例
        private readonly IRepository<BackgroundJobInfo, long> _backgroundJobRepository;

        #endregion
        #region 构造函数
        public BackgroundJobStore(IRepository<BackgroundJobInfo, long> backgroundJobRepository)
        {
            _backgroundJobRepository = backgroundJobRepository;
        }
        #endregion
        #region 方法

        
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return _backgroundJobRepository.DeleteAsync(jobInfo);
        }

        public Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            return _backgroundJobRepository.GetAsync(jobId);
        }
        [UnitOfWork]
        public Task<List<BackgroundJobInfo>> GetWaitJobsAsync(int maxResultCount)
        {
            var waitingJobs = _backgroundJobRepository.GetAll()
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
            return _backgroundJobRepository.InsertAsync(jobInfo);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return _backgroundJobRepository.UpdateAsync(jobInfo);
        }
        #endregion
    }
}
