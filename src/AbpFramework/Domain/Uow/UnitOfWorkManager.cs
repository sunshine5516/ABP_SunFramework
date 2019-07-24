using AbpFramework.Dependency;
using System.Linq;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 工作单元管理，实现IUnitOfWorkManager接口.
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager, ITransientDependency
    {
        #region 声明实例
        private readonly IIocResolver _iocResolver;
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly IUnitOfWorkDefaultOptions _defaultOptions;

        public IActiveUnitOfWork Current
        {
            get { return _currentUnitOfWorkProvider.Current; }
        }

        #endregion
        #region 构造函数
        public UnitOfWorkManager(IIocResolver iocResolver,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider,
            IUnitOfWorkDefaultOptions defaultOptions)
        {
            _iocResolver = iocResolver;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _defaultOptions = defaultOptions;
        }
        #endregion
        #region 方法
        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions { Scope = scope });
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
            options.FillDefaultsForNonProvidedOptions(_defaultOptions);
            var outerUow = _currentUnitOfWorkProvider.Current;
            if (options.Scope == TransactionScopeOption.Required
                && outerUow != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }
            var uow = _iocResolver.Resolve<IUnitOfWork>();
            uow.Completed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };
            uow.Failed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };
            uow.Disposed += (sender, args) =>
              {
                  _iocResolver.Release(uow);
              };
            //Inherit filters from outer UOW
            if (outerUow != null)
            {
                options.FillOuterUowFiltersForNonProvidedOptions(outerUow.Filters.ToList());
            }

            uow.Begin(options);
            //Inherit tenant from outer UOW
            if (outerUow != null)
            {
                uow.SetTenantId(outerUow.GetTenantId(), false);
            }

            _currentUnitOfWorkProvider.Current = uow;

            return uow;
        }

        #endregion
    }
}
