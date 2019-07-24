using AbpFramework.Dependency;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abp.Web.Mvc.Web.Mvc.Controllers
{
    /// <summary>
    /// 注册controller
    /// </summary>
    public class ControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 注册controllers
        /// </summary>
        /// <param name="context"></param>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //context.IocManager.IocContainer.Register(
            //     Classes.FromAssembly(context.Assembly).
            //       BasedOn<IController>().
            //       If(c => c.Name.EndsWith("Controller"))
            //       .LifestyleTransient());
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<Controller>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .LifestyleTransient()
                );

            //context.IocManager.IocContainer.Register(Castle.MicroKernel.Registration.Classes.FromThisAssembly()
            //    .BasedOn<IController>()
            //    .LifestylePerWebRequest()
            //    .Configure(x => x.Named(x.Implementation.FullName)));

            //context.IocManager.IocContainer.Register(
            //   Classes.FromAssembly(context.Assembly)
            //       .BasedOn<IController>()
            //       .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
            //       .LifestyleTransient()
            //   );
            //context.IocManager.IocContainer.Register(
            //   Classes.FromAssembly(context.Assembly)
            //       .BasedOn<Controller>()
            //       .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
            //       .LifestyleTransient()
            //   );
        }
    }
}
