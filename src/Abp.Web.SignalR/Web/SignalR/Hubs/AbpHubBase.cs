using AbpFramework.Configuration;
using AbpFramework.Dependency;
using AbpFramework.ObjectMapping;
using AbpFramework.Runtime.Session;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
namespace Abp.Web.SignalR.Web.SignalR.Hubs
{
    public abstract class AbpHubBase:Hub
    {
        public ILogger Logger { get; set; }
        public IAbpSession AbpSession { get; set; }

        public IIocResolver IocResolver { get; set; }

        public IObjectMapper ObjectMapper { get; set; }

        public ISettingManager SettingManager { get; set; }
        protected bool Disposed { get; private set; }
        protected AbpHubBase()
        {
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                Disposed = true;
                IocResolver?.Release(this);
            }
        }
    }
}
