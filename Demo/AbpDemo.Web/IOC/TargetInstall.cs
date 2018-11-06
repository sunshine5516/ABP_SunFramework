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

namespace AbpDemo.Web.IOC
{
    public class TargetInstall : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //实现IController接口  
            container.Register(Classes.FromThisAssembly().
                BasedOn<Controller>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                .LifestyleTransient());

            //注册拦截器  
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IInterceptor>());
        }
    }
}