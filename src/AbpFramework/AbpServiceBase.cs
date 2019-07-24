using AbpFramework.Configuration;
using AbpFramework.Domain.Uow;
using AbpFramework.ObjectMapping;
using Castle.Core.Logging;

namespace AbpFramework
{
    /// <summary>
    /// 抽象基类，
    /// 封装了对一些通用功能的调用。比如Setting,Localization和UnitOfWork功能。
    /// </summary>
    public abstract class AbpServiceBase
    {
        #region 声明实例
        private IUnitOfWorkManager _unitOfWorkManager;
        public ILogger Logger { protected get; set; }
        public ISettingManager SettingManager { get; set; }
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
        public IObjectMapper ObjectMapper { get; set; }
        #endregion
        #region 构造函数
        protected AbpServiceBase()
        {
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }
        #endregion
        #region 方法

        #endregion
    }
}
