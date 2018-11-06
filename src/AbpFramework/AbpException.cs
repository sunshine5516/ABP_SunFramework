using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework
{
    /// <summary>
    /// ABP异常处理基类
    /// </summary>
    public class AbpException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AbpException()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AbpException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message</param>
        public AbpException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public AbpException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
