using AbpFramework.Dependency;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Validation.Interception
{
    /// <summary>
    /// 验证拦截器
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        private readonly IIocResolver _iocResolver;
        public ValidationInterceptor(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
        }
        public void Intercept(IInvocation invocation)
        {
            using (var validator = _iocResolver.ResolveAsDisposable<MethodInvocationValidator>())
            {
                validator.Object.Initialize(invocation.MethodInvocationTarget, invocation.Arguments);
                validator.Object.Validate();
            }
            invocation.Proceed();
        }
    }
}
