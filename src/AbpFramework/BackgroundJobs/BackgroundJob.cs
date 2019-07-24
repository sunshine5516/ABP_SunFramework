using AbpFramework.Configuration;
using AbpFramework.Domain.Uow;
using Castle.Core.Logging;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 定义一个后台工作任务的基本实现。
    /// 具体的后台任务类可从BackgroundJob继承，这是定义最终需要被执行的逻辑的地方
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        #region 声明实例
        /// <summary>
        /// 设置管理引用.
        /// </summary>
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
        /// <summary>
        /// 获取当前的工作单元.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get {return UnitOfWorkManager.Current; } }
        /// <summary>
        /// 日志记录
        /// </summary>
        public ILogger Logger { protected get; set; }
        #endregion
        #region 构造函数
        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
        }
        #endregion
        #region 方法
        public abstract void Execute(TArgs args);
        #endregion
    }
}
