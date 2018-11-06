using System;
namespace AbpFramework.Auditing
{
    /// <summary>
    /// 用于标识一个方法或一个类的所有方法都需要关闭Auditing功能
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {
    }
}
