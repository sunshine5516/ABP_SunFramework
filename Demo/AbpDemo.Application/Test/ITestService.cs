using AbpFramework.Application.Services;
using System.Collections.Generic;
namespace AbpDemo.Application.Test
{
    public interface ITestService : IApplicationService
    {
        List<int> GetTestMethod();
        string GetAll();
        string GetById(int id);
    }
}
