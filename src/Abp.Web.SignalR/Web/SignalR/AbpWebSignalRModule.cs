using AbpFramework;
using AbpFramework.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Reflection;
namespace Abp.Web.SignalR.Web.SignalR
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpWebSignalRModule : AbpModule
    {
        public override void PreInitialize()
        {
            GlobalHost.DependencyResolver = new WindsorDependencyResolver(IocManager.IocContainer);
            UseAbpSignalRContractResolver();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        private void UseAbpSignalRContractResolver()
        {
            var serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    ContractResolver = new AbpSignalRContractResolver()
                });
            IocManager.IocContainer.Register(
                Component.For<JsonSerializer>().UsingFactoryMethod(() => serializer)
                );
        
        }
    }
}
