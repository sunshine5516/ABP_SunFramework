using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AbpFramework.Extensions;
namespace Abp.WebApi.Controllers.Dynamic
{
    /// <summary>
    /// 提供了一个Dictionary容器管理所有的DynamicApiControllerInfo对象
    /// </summary>
    public class DynamicApiControllerManager:ISingletonDependency
    {
        private readonly IDictionary<string, DynamicApiControllerInfo> _dynamicApiControllers;
        public DynamicApiControllerManager()
        {
            this._dynamicApiControllers = new Dictionary<string, DynamicApiControllerInfo>(StringComparer.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// 注册控制器信息
        /// </summary>
        /// <param name="controllerInfo"></param>
        public void Register(DynamicApiControllerInfo controllerInfo)
        {
            _dynamicApiControllers[controllerInfo.ServiceName] = controllerInfo;
        }
        /// <summary>
        /// 查找控制器
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public DynamicApiControllerInfo FindOrNull(string controllerName)
        {
            return _dynamicApiControllers.GetOrDefault(controllerName);
        }
        public IReadOnlyList<DynamicApiControllerInfo> GetAll()
        {
            return _dynamicApiControllers.Values.ToImmutableList();
        }
    }
}
