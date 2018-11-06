using AbpFramework.Auditing;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration.Startup
{
    public interface IAbpStartupConfiguration: IDictionaryBasedConfig
    {
        /// <summary>
        /// 注册容器
        /// </summary>
        IIocManager IocManager { get;}
        /// <summary>
        /// 获取对象实例
        /// </summary>
        T Get<T>();
        IValidationConfiguration Validation { get; }

        /// <summary>
        /// 缓存配置
        /// </summary>
        ICachingConfiguration Caching { get; }
        IModuleConfigurations Modules { get; }
        string DefaultNameOrConnectionString { get; set; }
        /// <summary>
        /// Used to replace a service type.
        /// Given <see cref="replaceAction"/> should register an implementation for the <see cref="type"/>.
        /// </summary>
        /// <param name="type">The type to be replaced.</param>
        /// <param name="replaceAction">Replace action.</param>
        void ReplaceService(Type type, Action replaceAction);
        /// <summary>
        /// UOW默认配置
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }
        /// <summary>
        /// 审计配置
        /// </summary>
        IAuditingConfiguration Auditing { get; }
    } 
}
