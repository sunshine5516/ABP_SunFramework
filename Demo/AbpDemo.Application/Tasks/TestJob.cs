using AbpFramework.BackgroundJobs;
using AbpFramework.Dependency;
using System;
namespace AbpDemo.Application.Tasks
{
    public class TestJob : BackgroundJob<int>, ITransientDependency
    {
        public override void Execute(int args)
        {
            Logger.Debug(args.ToString());
        }
    }
}
