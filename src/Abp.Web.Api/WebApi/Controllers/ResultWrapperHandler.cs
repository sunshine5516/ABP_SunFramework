using Abp.WebApi.Configuration;
using AbpFramework.Dependency;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.WebApi.Controllers
{
    public class ResultWrapperHandler : DelegatingHandler, ITransientDependency
    {
        private readonly IAbpWebApiConfiguration _configuration;
        public ResultWrapperHandler(IAbpWebApiConfiguration configuration)
        {
            this._configuration = configuration;
        }
        protected override async Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            //result.m
            return result;
        }
    }
}
