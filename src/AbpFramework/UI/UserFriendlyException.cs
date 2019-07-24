using AbpFramework.Logging;
using System;
using System.Runtime.Serialization;
namespace AbpFramework.UI
{
    /// <summary>
    /// 该异常直接展示给用户.
    /// </summary>
    [Serializable]
    public class UserFriendlyException : AbpException, IHasLogSeverity, IHasErrorCode
    {
        public string Details { get; private set; }
        
        public LogSeverity Severity { get; set; }
        public int Code { get; set; }
        #region 构造函数
        public UserFriendlyException()
        {
            Severity = LogSeverity.Warn;
        }
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }
        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }
        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }
        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }
        #endregion
    }
}
