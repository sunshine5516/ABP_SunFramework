using Castle.Core.Logging;
using System.Net.Mail;
using System.Threading.Tasks;
namespace AbpFramework.Net.Mail
{
    /// <summary>
    /// 邮件发送默认实现
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        #region 声明实例
        public ILogger Logger { get; set; }
        #endregion
        #region 构造函数
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }
        #endregion
        protected override void SendEmail(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }
        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}
