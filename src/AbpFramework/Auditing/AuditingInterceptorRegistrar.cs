using AbpFramework.Dependency;
using Castle.Core;
using System;
using System.Linq;
using System.Reflection;
namespace AbpFramework.Auditing
{
    /// <summary>
    /// 注册<see cref="AuditingInterceptor"/>拦截器
    /// </summary>
    public static class AuditingInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
              {                  
                  if (!iocManager.IsRegistered<IAuditingConfiguration>())
                  {
                      return;
                  }
                  var auditingConfiguration = iocManager.Resolve<IAuditingConfiguration>();
                  if (ShouldIntercept(auditingConfiguration, handler.ComponentModel.Implementation))
                  {
                      handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuditingInterceptor)));
                  }
              };
        }
        private static bool ShouldIntercept(IAuditingConfiguration auditingConfiguration, Type type)
        {
            if (auditingConfiguration.Selectors.Any(selector => selector.Predicate(type)))
            {
                return true;
            }

            if (type.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}
