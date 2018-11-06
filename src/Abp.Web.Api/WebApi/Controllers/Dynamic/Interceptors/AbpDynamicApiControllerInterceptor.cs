using Abp.WebApi.Controllers.Dynamic.Builders;
using AbpFramework.Extensions;
using Castle.DynamicProxy;
using System.Reflection;
namespace Abp.WebApi.Controllers.Dynamic.Interceptors
{
    /// <summary>
    /// 实现了Castle的IInterceptor。作为动态生成的DynamicApiController<T>的拦截器，
    /// 它拦截所有对action的调用，然后通过反射调用底层真实的IApplicationService对象的方法。
    /// 拦截器动态控制器.
    /// 它处理对动态生成的api控制器的方法调用 以及调用底层代理对象
    /// </summary>
    /// <typeparam name="T">Type of the proxied object</typeparam>
    public class AbpDynamicApiControllerInterceptor<T> : IInterceptor
    {
        /// <summary>
        /// 实际对象实例在调用动态控制器的方法时调用它的方法。
        /// </summary>
        private readonly T _proxiedObject;
        public AbpDynamicApiControllerInterceptor(T proxiedObject)
        {
            this._proxiedObject = proxiedObject;
        }
        public void Intercept(IInvocation invocation)
        {
            if (DynamicApiControllerActionHelper.IsMethodOfType(invocation.Method, typeof(T)))
            {
                try
                {
                    //invocation.ReturnValue = "hello world";
                    invocation.ReturnValue=invocation.Method.Invoke(_proxiedObject, invocation.Arguments);
                }
                catch (TargetInvocationException targetInvocation)
                {
                    if (targetInvocation.InnerException != null)
                    {
                        targetInvocation.InnerException.ReThrow();
                    }

                    throw;
                }
            }
            else
            {
                //Call api controller's methods as usual.
                invocation.Proceed();
            }
        }
    }
}
