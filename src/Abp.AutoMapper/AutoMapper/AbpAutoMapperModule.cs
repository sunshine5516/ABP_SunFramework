using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection;
using AbpFramework.Configuration.Startup;
using AutoMapper;
using System;
using Castle.MicroKernel.Registration;
using System.Reflection;
namespace Abp.AutoMapper.AutoMapper
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpAutoMapperModule : AbpModule
    {
        #region 声明实例
        private readonly ITypeFinder _typeFinder;
        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();
        #endregion
        #region 构造函数
        public AbpAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }
        #endregion
        #region 方法
        public override void PreInitialize()
        {
            IocManager.Register<IAbpAutoMapperConfiguration, AbpAutoMapperConfiguration>();
            Configuration.ReplaceService<AbpFramework.ObjectMapping.IObjectMapper, AutoMapperObjectMapper>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CreateCoreMappings);
        }
        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                  {
                      FindAndAutoMapTypes(configuration);
                      foreach(var configurator in Configuration.Modules.AbpAutoMapper().Configurators)
                      {
                          configurator(configuration);
                      }
                  };
                if (Configuration.Modules.AbpAutoMapper().UseStaticMapper)
                {
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance)
                        .LifestyleSingleton());
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapToAttribute));
            }
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }

        private void CreateCoreMappings(IMapperConfigurationExpression configuration)
        {
            
        }
        #endregion
    }
}
