using Abp.WebApi.Controllers.Dynamic.Formatters;
using AbpFramework.Auditing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Abp.WebApi.Controllers.Dynamic.Scripting
{
    /// <summary>
    /// 此类用于创建代理以从Javascript客户端调用动态api方法。
    /// </summary>
    [DisableAuditing]
    //[DisableAbpAntiForgeryTokenValidation]
    public class AbpServiceProxiesController: AbpApiController
    {
        #region 声明实例
        private readonly ScriptProxyManager _scriptProxyManager;
        #endregion
        #region 构造函数
        public AbpServiceProxiesController(ScriptProxyManager scriptProxyManager)
        {
            this._scriptProxyManager = scriptProxyManager;
        }
        #endregion
        #region 方法
        /// <summary>
        /// 获取给定服务名称的javascript代理。
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="type">Script type</param>
        public HttpResponseMessage Get(string name, ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetScript(name, type);
            var response = Request.CreateResponse(HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType=new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
        public HttpResponseMessage GetALL(ProxyScriptType type=ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetAllScript(type);
            var response = Request.CreateResponse(HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType=new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
        #endregion
    }
}
