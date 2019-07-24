using AbpFramework.Dependency;
using Hangfire;
using System;
using System.Collections.Generic;
namespace Abp.Hangfire.Hangfire
{
    /// <summary>
    /// 继承了Hangfire组件里的JobActivator.
    /// </summary>
    public class HangfireIocJobActivator:JobActivator
    {
        private readonly IIocResolver _iocResolver;
        #region 构造函数
        public HangfireIocJobActivator(IIocResolver iocResolver)
        {
            if (iocResolver == null)
            {
                throw new ArgumentNullException(nameof(iocResolver));
            }
            _iocResolver = iocResolver;
        }
        #endregion
        #region 方法
        public override object ActivateJob(Type jobType)
        {
            return _iocResolver.Resolve(jobType);
        }
        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            return new HangfireIocJobActivatorScope(this,_iocResolver);
        }
        #endregion
        class HangfireIocJobActivatorScope : JobActivatorScope
        {
            private readonly JobActivator _activator;
            private readonly IIocResolver _iocResolver;
            private readonly List<object> _resolvedObjects;
            public HangfireIocJobActivatorScope(JobActivator activator, IIocResolver iocResolver)
            {
                _activator = activator;
                _iocResolver = iocResolver;
                _resolvedObjects = new List<object>();
            }
            public override object Resolve(Type type)
            {
                var instance = _activator.ActivateJob(type);
                _resolvedObjects.Add(instance);
                return instance;
            }
            public override void DisposeScope()
            {
                _resolvedObjects.ForEach(_iocResolver.Release);
            }
        }
    }
}
