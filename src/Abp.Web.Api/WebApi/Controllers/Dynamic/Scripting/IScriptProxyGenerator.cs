namespace Abp.WebApi.Controllers.Dynamic.Scripting
{
    /// <summary>
    /// 定义了一个generate方法用于生成访问Dynamic WebApi的代理，所谓代理就是一段js代码。
    /// </summary>
    internal interface IScriptProxyGenerator
    {
        string Generate();
    }
}
