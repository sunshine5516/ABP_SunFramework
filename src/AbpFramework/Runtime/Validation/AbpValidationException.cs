using AbpFramework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace AbpFramework.Runtime.Validation
{
    /// <summary>
    /// 验证异常
    /// </summary>
    [Serializable]
    public class AbpValidationException : AbpException, IHasLogSeverity
    {
        /// <summary>
        /// 验证错误的详细列表。
        /// </summary>
        public IList<ValidationResult> ValidationErrors { get; set; }
        /// <summary>
        /// 异常等级
        /// </summary>
        public LogSeverity Severity { get; set; }
        #region 构造函数
        public AbpValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
        public AbpValidationException(SerializationInfo serializationInfo, StreamingContext context)
            :base(serializationInfo,context)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
        public AbpValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
        public AbpValidationException(string message, IList<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
            Severity = LogSeverity.Warn;
        }
        public AbpValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }
        #endregion
    }
}
