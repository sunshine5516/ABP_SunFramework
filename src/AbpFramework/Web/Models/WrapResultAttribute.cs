using System;
namespace AbpFramework.Web.Models
{
    /// <summary>
    /// 用于确定ABP应如何在Web层上包装响应。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute:Attribute
    {
        public bool WrapOnSuccess { get; set; }
        public bool WrapOnError { get; set; }
        public bool LogError { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="wrapOnSuccess">Wrap result on success.</param>
        /// <param name="wrapOnError">Wrap result on error.</param>
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;

            LogError = true;
        }
    }
}
