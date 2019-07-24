using AbpFramework.Dependency;
using Hangfire.Common;
using Hangfire.Server;
using System;
namespace Abp.Hangfire.Hangfire
{
    public class AbpHangfireJobExceptionFilter : JobFilterAttribute, IServerFilter, ITransientDependency
    {
        public void OnPerformed(PerformedContext filterContext)
        {
            
        }

        public void OnPerforming(PerformingContext filterContext)
        {
            
        }
    }
}
