using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    public interface IDynamicApiControllerBuilder
    {
        IApiControllerBuilder<T> For<T>(string serviceName);
        IBatchApiControllerBuilder<T> ForAll<T>(Assembly assembly, string servicePrefix);
    }
}
