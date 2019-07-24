using System.Text;
namespace Abp.WebApi.Controllers.Dynamic.Scripting.Angular
{
    /// <summary>
    /// 根据DynamicApiControllerInfo生成访问Dynamic WebApi方法的AngularJs代理
    /// </summary>
    public class AngularProxyGenerator : IScriptProxyGenerator
    {
        #region 声明实例
        private readonly DynamicApiControllerInfo _controllerInfo;
        #endregion
        #region 构造函数
        public AngularProxyGenerator(DynamicApiControllerInfo controllerInfo)
        {
            _controllerInfo = controllerInfo;
        }
        #endregion
        #region 方法
        public string Generate()
        {
            var script = new StringBuilder();

            script.AppendLine("(function (abp, angular) {");
            script.AppendLine("");
            script.AppendLine("    if (!angular) {");
            script.AppendLine("        return;");
            script.AppendLine("    }");
            script.AppendLine("    ");
            script.AppendLine("    var abpModule = angular.module('abp');");
            script.AppendLine("    ");
            script.AppendLine("    abpModule.factory('abp.services." + _controllerInfo.ServiceName.Replace("/", ".") + "', [");
            script.AppendLine("        '$http', function ($http) {");
            script.AppendLine("            return new function () {");

            foreach (var methodInfo in _controllerInfo.Actions.Values)
            {
                var actionWriter = new AngularActionScriptWriter(_controllerInfo, methodInfo);
                actionWriter.WriteTo(script);
            }

            script.AppendLine("            };");
            script.AppendLine("        }");
            script.AppendLine("    ]);");
            script.AppendLine();

            script.AppendLine();
            script.AppendLine("})((abp || (abp = {})), (angular || undefined));");

            return script.ToString();
        }
        #endregion

    }
}
