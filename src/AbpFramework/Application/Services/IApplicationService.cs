using AbpFramework.Dependency;
namespace AbpFramework.Application.Services
{
    /// <summary>
    ///空接口，起标识作用。所有实现了IApplicationService 的类都会被自动注入到容器中。
    ///同时所有IApplicationService对象都会被注入一些拦截器（例如：auditing, UnitOfWork等）
    ///以实现AOP
    public interface IApplicationService: ITransientDependency
    {
    }
}
