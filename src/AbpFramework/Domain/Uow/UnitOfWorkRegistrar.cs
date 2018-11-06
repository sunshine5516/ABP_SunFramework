using AbpFramework.Dependency;
using Castle.Core;
using Castle.MicroKernel;
using System.Linq;
using System.Reflection;
namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 用于为Unit Of Work机制注册所需类的拦截器。
    /// 在AbpBootstrapper通过<see cref="AddInterceptorRegistrars"/>方法注册拦截器
    /// </summary>
    public static class UnitOfWorkRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
                  {
                      var implementationType = handler.ComponentModel.Implementation.GetTypeInfo();
                      HandleTypesWithUnitOfWorkAttribute(implementationType, handler);
                      HandleConventionalUnitOfWorkTypes(iocManager, implementationType, handler);
                  };
        }
        private static void HandleTypesWithUnitOfWorkAttribute(TypeInfo implementationType, IHandler handler)
        {
            if (IsUnitOfWorkType(implementationType) || AnyMethodHasUnitOfWork(implementationType))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
        private static void HandleConventionalUnitOfWorkTypes(IIocManager iocManager, TypeInfo implementationType, IHandler handler)
        {
            if (!iocManager.IsRegistered<IUnitOfWorkDefaultOptions>())
            {
                return;
            }

            var uowOptions = iocManager.Resolve<IUnitOfWorkDefaultOptions>();

            if (uowOptions.IsConventionalUowClass(implementationType.AsType()))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
        private static bool IsUnitOfWorkType(TypeInfo implementationType)
        {
            return UnitOfWorkHelper.HasUnitOfWorkAttribute(implementationType);
        }
        private static bool AnyMethodHasUnitOfWork(TypeInfo implementationType)
        {
            return implementationType
               .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
               .Any(UnitOfWorkHelper.HasUnitOfWorkAttribute);
        }
    }
}
