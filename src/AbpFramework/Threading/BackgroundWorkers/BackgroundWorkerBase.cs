using AbpFramework.Configuration;
using AbpFramework.Domain.Uow;
using Castle.Core.Logging;
namespace AbpFramework.Threading.BackgroundWorkers
{
    /// <summary>
    /// 实现IBackgroundWorker的一个抽象类，同时添加了UOW,Setting 和本地化的一些辅助方法。
    /// </summary>
    public abstract class BackgroundWorkerBase : RunnableBase, IBackgroundWorker
    {
        #region 声明实例
        public ISettingManager SettingManager { protected get; set; }
        private IUnitOfWorkManager _unitOfWorkManager;
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if(_unitOfWorkManager==null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }
                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
        public ILogger Logger { protected get; set; }
        #endregion
        #region 构造函数
        protected BackgroundWorkerBase()
        {
            Logger = NullLogger.Instance;
        }
        #endregion
        #region 方法
        public override void Start()
        {
            base.Start();
            Logger.Debug("Start background worker: " + ToString());
        }

        public override void Stop()
        {
            base.Stop();
            Logger.Debug("Stop background worker: " + ToString());
        }

        public override void WaitToStop()
        {
            base.WaitToStop();
            Logger.Debug("WaitToStop background worker: " + ToString());
        }
        public override string ToString()
        {
            return GetType().FullName;
        }
        #endregion

    }
}
