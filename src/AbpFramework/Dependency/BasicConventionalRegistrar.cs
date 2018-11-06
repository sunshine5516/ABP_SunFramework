using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    ///  用来注册基本的依赖实现，比如<see cref ="ITransientDependency"/>
    /// 和<see cref ="ISingletonDependency"/>。
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
           
            //注册拦截器  
            //context.IocManager.IocContainer.Register(
            //    Classes.FromThisAssembly()
            //    .BasedOn<IInterceptor>()
            //    .WithService.Self()
            //    .LifestyleTransient());

            //Transient
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ISingletonDependency>()
                .If(type=>!type.GetTypeInfo().IsGenericTypeDefinition)
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleSingleton()
                );
            //Windsor Interceptors
            context.IocManager.IocContainer.Register(
              Classes.FromAssembly(context.Assembly)
                  .IncludeNonPublicTypes()
                  .BasedOn<IInterceptor>()
                  .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                  .WithService.Self()
                  .LifestyleTransient()
              );

        }
    }
}
