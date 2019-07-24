using Abp.Web.Common.Web.Api.ProxyScripting.Generators;
using AbpFramework.Extensions;
using System.Text;
namespace Abp.WebApi.Controllers.Dynamic.Scripting.jQuery
{
    /// <summary>
    /// 根据DynamicApiControllerInfo生成访问Dynamic WebApi方法的JQuery代理。
    /// </summary>
    internal class JQueryProxyGenerator : IScriptProxyGenerator
    {
        #region 声明实例
        private readonly DynamicApiControllerInfo _controllerInfo;
        private readonly bool _defineAmdModule;
        #endregion
        #region 构造函数
        public JQueryProxyGenerator(DynamicApiControllerInfo controllerInfo, bool defineAmdModule = true)
        {
            _controllerInfo = controllerInfo;
            _defineAmdModule = defineAmdModule;
        }
        #endregion
        #region 方法
        public string Generate()
        {
            var script = new StringBuilder();
            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine("    var serviceNamespace = abp.utils.createNamespace(abp, 'services." + _controllerInfo.ServiceName.Replace("/", ".") + "');");
            script.AppendLine();
            foreach(var methodInfo in _controllerInfo.Actions.Values)
            {
                AppendMethod(script, _controllerInfo, methodInfo);
                script.AppendLine();
            }
            //generate amd module definition
            if (_defineAmdModule)
            {
                script.AppendLine("    if(typeof define === 'function' && define.amd){");
                script.AppendLine("        define(function (require, exports, module) {");
                script.AppendLine("            return {");
                var methodNo = 0;
                foreach (var methodInfo in _controllerInfo.Actions.Values)
                {
                    script.AppendLine("                '" + methodInfo.ActionName.ToCamelCase() + "' : serviceNamespace" + 
                        ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(methodInfo.ActionName.ToCamelCase()) + ((methodNo++) < (_controllerInfo.Actions.Count - 1) ? "," : ""));
                }
                script.AppendLine("            };");
                script.AppendLine("        });");
                script.AppendLine("    }");
            }
            script.AppendLine();
            script.AppendLine("})();");
            return script.ToString();
        }
        private static void AppendMethod(StringBuilder script,
            DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo methodInfo)
        {
            var generator=new JQueryActionScriptGenerator(controllerInfo, methodInfo);
            script.AppendLine(generator.GenerateMethod());
        }
        #endregion

    }
}
