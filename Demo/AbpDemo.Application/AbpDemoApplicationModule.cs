using Abp.AutoMapper.AutoMapper;
using Abp.Dapper.Dapper;
using Abp.MailKit;
using Abp.Zero.Common.Authorization.Roles;
using AbpDemo.Application.Roles.Dto;
using AbpDemo.Core;
using AbpDemo.Core.Authorization.Roles;
using AbpFramework.Authorization;
using AbpFramework.Modules;
using Castle.MicroKernel.Registration;
using System.Reflection;
namespace AbpDemo.Application
{
    [DependsOn(typeof(AbpDemoCoreModule),typeof(AbpAutoMapperModule)
        , typeof(AbpDapperModule),typeof(AbpMailKitModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
            //base.PreInitialize();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //注册IDtoMapping
            IocManager.IocContainer.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .IncludeNonPublicTypes()
                .BasedOn<IDtoMapping>()
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleTransient()
                );
            //遍历实现IDtoMapping接口类，解析依赖
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                var mappers = IocManager.IocContainer.ResolveAll<IDtoMapping>();
                foreach (var dtomap in mappers)
                    dtomap.CreateMapping(mapper);
            });
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {

                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
            });
        }
    }
}
