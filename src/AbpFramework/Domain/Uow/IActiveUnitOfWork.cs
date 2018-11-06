using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    ///一个UOW除了以上两个接口中定义的方法和属性外，其他的属性和方法都在这个接口定义的。
    ///比如Completed，Disposed，Failed事件代理，Filter的enable和disable,以及同步、异步的SaveChanges方法。
    ///  这个接口不能被注入或直接使用。使用 <see cref="IUnitOfWorkManager"/> 实现.
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// 当这个UOW成功完成时，这个事件被调用。
        /// </summary>
        event EventHandler Completed;
        /// <summary>
        /// 当这个UOW未成功完成时，这个事件被调用。
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 当这个UOW被释放时，这个事件被调用。
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// 获取这个工作单元是否是事务性的。
        /// </summary>
        UnitOfWorkOptions Options { get; }
        /// <summary>
        /// 获取此工作单元的数据过滤器配置
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// 是否被释放
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// 保存更改
        /// This method may be called to apply changes whenever needed.
        /// 请注意，如果这个工作单元是事务性的，那么如果事务回滚，保存的更改也会回滚。
        /// SaveChanges通常不需要显式调用, 因为所有更改都会在工作单元的末尾自动保存        
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// 异步保存更改
        /// This method may be called to apply changes whenever needed.
        /// 请注意，如果这个工作单元是事务性的，那么如果事务回滚，保存的更改也会回滚。
        /// SaveChanges通常不需要显式调用, 因为所有更改都会在工作单元的末尾自动保存        
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// 禁用一个或多个数据过滤器。
        /// 如果过滤器已被禁用，则不执行任何操作。
        /// 如果需要，在using语句中使用此方法重新启用筛选器。
        /// </summary>
        /// <param name="filterNames">名称集合.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect.</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        /// 禁用一个或多个数据过滤器。
        /// 如果过滤器已被禁用，则不执行任何操作。
        /// 如果需要，在using语句中使用此方法重新启用筛选器。
        ///  </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        /// 检查过滤器是否可用。
        /// </summary>
        /// <param name="filterName">Name of the filter. <see cref="AbpDataFilters"/> for standard filters.</param>
        bool IsFilterEnabled(string filterName);


        /// <summary>
        /// 设置（重写）过滤器参数的值。
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">值</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);

        /// <summary>
        /// 设置/更改此UOW的租户的ID。
        /// </summary>
        /// <param name="tenantId">tenant id.</param>
        /// <returns></returns>
        IDisposable SetTenantId(int? tenantId);

        /// <summary>
        /// 设置/更改此UOW的租户的ID。
        /// </summary>
        /// <param name="tenantId">tenant id</param>
        /// <param name="switchMustHaveTenantEnableDisable">
        /// True to enable/disable <see cref="IMustHaveTenant"/> based on given tenantId.
        /// Enables <see cref="IMustHaveTenant"/> filter if tenantId is not null.
        /// Disables <see cref="IMustHaveTenant"/> filter if tenantId is null.
        /// This value is true for <see cref="SetTenantId(int?)"/> method.
        /// </param>
        /// <returns>A disposable object to restore old TenantId value when you dispose it</returns>
        IDisposable SetTenantId(int? tenantId, bool switchMustHaveTenantEnableDisable);

        /// <summary>
        /// 获取.TenantId
        /// </summary>
        /// <returns></returns>
        int? GetTenantId();
    }
}
