using AbpFramework.Dependency;
using System;
namespace AbpFramework.Configuration.Startup
{
    public static class AbpStartupConfigurationExtensions
    {
        public static void ReplaceService<TType>(this IAbpStartupConfiguration configuration, Action replaceAction)
            where TType : class
        {
            configuration.ReplaceService(typeof(TType), replaceAction);
        }
        /// <summary>
        /// 替换服务类型.
        /// </summary>
        /// <typeparam name="TType">服务接口.</typeparam>
        /// <typeparam name="TImpl">接口实现.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="lifeStyle">Life style.</param>
        public static void ReplaceService<TType, TImpl>(this IAbpStartupConfiguration configuration, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            configuration.ReplaceService(typeof(TType), () =>
             {
                 configuration.IocManager.Register<TType, TImpl>(lifeStyle);
             });
        }
    }
}
