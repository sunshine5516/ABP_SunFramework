using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
namespace AbpFramework.Threading.BackgroundWorkers
{
    /// <summary>
    /// 管理后台工作任务接口的实现
    /// </summary>
    public class BackgroundWorkerManager : RunnableBase, IBackgroundWorkerManager, ISingletonDependency, IDisposable
    {
        #region 声明实例
        private readonly IIocResolver _iocResolver;
        private readonly List<IBackgroundWorker> _backgroundJobs;
        private bool _isDisposed;
        #endregion
        #region 构造函数
        public BackgroundWorkerManager(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
            _backgroundJobs = new List<IBackgroundWorker>();
        }
        #endregion
        #region 方法
        public override void Start()
        {
            base.Start();
            _backgroundJobs.ForEach(job => job.Start());
        }
        public override void Stop()
        {
            _backgroundJobs.ForEach(job => job.Stop());
            base.Stop();
        }
        public override void WaitToStop()
        {
            _backgroundJobs.ForEach(job => job.WaitToStop());
            base.WaitToStop();
        }
        public void Add(IBackgroundWorker worker)
        {
            _backgroundJobs.Add(worker);
            if(IsRunning)
            {
                worker.Start();
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            _backgroundJobs.ForEach(_iocResolver.Release);
            _backgroundJobs.Clear();
        }
        #endregion

    }
}
