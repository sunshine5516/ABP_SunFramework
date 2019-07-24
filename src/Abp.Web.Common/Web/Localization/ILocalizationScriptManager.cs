using System.Globalization;

namespace Abp.Web.Common.Web.Localization
{
    /// <summary>
    /// 获取本地化javascript接口.
    /// </summary>
    public interface ILocalizationScriptManager
    {
        /// <summary>
        /// 获取包含当前文化中所有本地化信息的Javascript.
        /// </summary>
        string GetScript();

        /// <summary>
        /// 获取包含给定文化中的所有本地化信息的Javascript。
        /// </summary>
        /// <param name="cultureInfo">Culture to get script</param>
        string GetScript(CultureInfo cultureInfo);
    }
}
