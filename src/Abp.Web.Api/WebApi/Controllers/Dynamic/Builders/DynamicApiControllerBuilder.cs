using AbpFramework.Dependency;
using System.Reflection;
namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    public class DynamicApiControllerBuilder : IDynamicApiControllerBuilder
    {
        private readonly IIocResolver _iocResolver;
        public DynamicApiControllerBuilder(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }
        /// <summary>
        /// 为给定类型生成一个新的动态api控制器。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public IApiControllerBuilder<T> For<T>(string serviceName)
        {
            return new ApiControllerBuilder<T>(serviceName, _iocResolver);
        }
        /// <summary>
        /// 生成多个动态API控制器.
        /// </summary>
        /// <typeparam name="T">Base type (class or interface) for services</typeparam>
        /// <param name="assembly">程序集包含的类型</param>
        /// <param name="servicePrefix">Service prefix</param>
        public IBatchApiControllerBuilder<T> ForAll<T>(Assembly assembly, string servicePrefix)
        {
            return new BatchApiControllerBuilder<T>(_iocResolver, this, assembly, servicePrefix);
        }
    }
}
