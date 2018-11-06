using AbpFramework.Dependency;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AbpDemo.Web
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
              Classes.FromThisAssembly()
                  .IncludeNonPublicTypes()
                  .BasedOn<IInterceptor>()
                  .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                  .WithService.Self()
                  .LifestyleTransient()
              );

            //Transient
            container.Register(
                Classes.FromThisAssembly()
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );


            container.Register(Classes.FromThisAssembly() //在哪里找寻接口或类
                .BasedOn<IController>() //实现IController接口
                .If(t => t.Name.EndsWith("Controller")) //以"Controller"结尾
                .Configure(c => c.LifestylePerWebRequest()));//每次请求创建一个Controller实例
        }
    }
}