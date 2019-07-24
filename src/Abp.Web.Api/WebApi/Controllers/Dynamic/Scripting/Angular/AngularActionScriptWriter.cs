using Abp.Web.Common.Web;
using Abp.Web.Common.Web.Api.ProxyScripting.Generators;
using AbpFramework.Extensions;
using System.Globalization;
using System.Text;
namespace Abp.WebApi.Controllers.Dynamic.Scripting.Angular
{
    public class AngularActionScriptWriter
    {
        #region 声明实例
        private readonly DynamicApiControllerInfo _controllerInfo;
        private readonly DynamicApiActionInfo _actionInfo;
        #endregion
        #region 构造函数
        public AngularActionScriptWriter(DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo methodInfo)
        {
            _controllerInfo = controllerInfo;
            _actionInfo = methodInfo;
        }
        #endregion
        #region 方法
        public virtual void WriteTo(StringBuilder script)
        {
            script.AppendLine("                this" + ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(_actionInfo.ActionName.ToCamelCase()) + " = function (" + ActionScriptingHelper.GenerateJsMethodParameterList(_actionInfo.Method, "httpParams") + ") {");
            script.AppendLine("                    return $http(angular.extend({");
            script.AppendLine("                        url: abp.appPath + '" + ActionScriptingHelper.GenerateUrlWithParameters(_controllerInfo, _actionInfo) + "',");
            script.AppendLine("                        method: '" + _actionInfo.Verb.ToString().ToUpper(CultureInfo.InvariantCulture) + "',");

            if (_actionInfo.Verb == HttpVerb.Get)
            {
                script.AppendLine("                        params: " + ActionScriptingHelper.GenerateBody(_actionInfo));
            }
            else
            {
                script.AppendLine("                        data: JSON.stringify(" + ActionScriptingHelper.GenerateBody(_actionInfo) + ")");
            }

            script.AppendLine("                    }, httpParams));");
            script.AppendLine("                };");
            script.AppendLine("                ");
        }
        #endregion
    }
}
