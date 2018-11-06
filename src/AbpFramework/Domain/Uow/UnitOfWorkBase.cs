using AbpFramework.Extensions;
using AbpFramework.Utils.Etc;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// UOW基类
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        #region 声明实例
        public string Id { get; }
        [DoNotWire]
        public IUnitOfWork Outer { get; set; }

        public UnitOfWorkOptions Options { get; private set; }
        private readonly List<DataFilterConfiguration> _filters;
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }

        public bool IsDisposed { get; private set; }

        public event EventHandler Completed;
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;
        public event EventHandler Disposed;

        /// <summary>
        /// UOW是否成功
        /// </summary>
        private bool _succeed;
        private bool _isBeginCalledBefore;
        private bool _isCompleteCalledBefore;
        private Exception _exception;
        /// <summary>
        /// 获取默认UOW options.
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }
        private int? _tenantId;
        /// <summary>
        /// 获取连接字符串解析
        /// </summary>
        protected IConnectionStringResolver ConnectionStringResolver { get; }
        protected IUnitOfWorkFilterExecuter FilterExecuter { get; }
        #endregion
        #region 构造函数
        public UnitOfWorkBase(
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter)
        {
            ConnectionStringResolver = connectionStringResolver;
            DefaultOptions = defaultOptions;
            FilterExecuter = filterExecuter;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
        }
        #endregion
        #region 方法
        public void Begin(UnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));
            PreventMultipleBegin();
            Options = options;
            SetFilters(options.FilterOverrides);
            //Set
            BeginUow();
        }

        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }
        /// <summary>
        /// 需要被派生类继承
        /// </summary>
        protected abstract void CompleteUow();
        /// <summary>
        /// 需要被派生类继承
        /// </summary>
        protected abstract Task CompleteUowAsync();
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }
        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }
        public IDisposable DisableFilter(params string[] filterNames)
        {
            var disabledFilters = new List<string>();
            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], false);
                }
            }
            disabledFilters.ForEach(ApplyDisableFilter);
            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));            
        }

        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }
            IsDisposed = true;
            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }
        public abstract void DisposeUow();
        public IDisposable EnableFilter(params string[] filterNames)
        {
            var enabledFilters = new List<string>();
            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], true);
                }
            }
            enabledFilters.ForEach(ApplyEnableFilter);
            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        public int? GetTenantId()
        {
            return _tenantId;
        }

        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        public abstract void SaveChanges();

        public abstract Task SaveChangesAsync();

        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);
            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(parameterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[parameterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });            
        }

        public IDisposable SetTenantId(int? tenantId)
        {
            return SetTenantId(tenantId, true);
        }

        public IDisposable SetTenantId(int? tenantId, bool switchMustHaveTenantEnableDisable)
        {
            var oldTenantId = _tenantId;
            _tenantId = tenantId;


            IDisposable mustHaveTenantEnableChange;
            if (switchMustHaveTenantEnableDisable)
            {
                mustHaveTenantEnableChange = tenantId == null
                    ? DisableFilter(AbpDataFilters.MustHaveTenant)
                    : EnableFilter(AbpDataFilters.MustHaveTenant);
            }
            else
            {
                mustHaveTenantEnableChange = NullDisposable.Instance;
            }

            var mayHaveTenantChange = SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, tenantId);
            var mustHaveTenantChange = SetFilterParameter(AbpDataFilters.MustHaveTenant, AbpDataFilters.Parameters.TenantId, tenantId ?? 0);

            return new DisposeAction(() =>
            {
                mayHaveTenantChange.Dispose();
                mustHaveTenantChange.Dispose();
                mustHaveTenantEnableChange.Dispose();
                _tenantId = oldTenantId;
            });
        }
        #endregion
        #region 辅助方法
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new Exception("This unit of work has started before. Can not call Start method more than once.");
            }
            _isBeginCalledBefore = true;
        }
        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (int i = 0; i < _filters.Count; i++)
            {
                var filterOverride= filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }
        }

        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return ConnectionStringResolver.GetNameOrConnectionString(args);
        }
        /// Can be implemented by derived classes to start UOW.
        /// </summary>
        protected virtual void BeginUow()
        {

        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new Exception("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filter;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new Exception("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }
        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new Exception("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filterIndex;
        }

        protected virtual void ApplyDisableFilter(string filterName)
        {
            FilterExecuter.ApplyDisableFilter(this, filterName);
        }

        protected virtual void ApplyEnableFilter(string filterName)
        {
            FilterExecuter.ApplyEnableFilter(this, filterName);
        }

        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            FilterExecuter.ApplyFilterParameterValue(this, filterName, parameterName, value);
        }

        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }


        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        #endregion
    }
}
