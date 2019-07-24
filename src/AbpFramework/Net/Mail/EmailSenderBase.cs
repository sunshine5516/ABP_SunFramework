using AbpFramework.Extensions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace AbpFramework.Net.Mail
{
    /// <summary>
    /// 发送邮件基类
    /// </summary>
    public abstract class EmailSenderBase:IEmailSender
    {
        #region 声明实例
        public IEmailSenderConfiguration Configuration { get; }
        #endregion
        #region 构造函数
        protected EmailSenderBase(IEmailSenderConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion
        #region 方法

        public virtual async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage
            {
                To = {to},
                Subject=subject,
                Body=body,
                IsBodyHtml=isBodyHtml
            });
        }

        public virtual  void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public virtual void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public virtual void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            SendEmail(mail);
        }

        public virtual async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if(normalize)
            {
                NormalizeMail(mail);
            }
            await SendEmailAsync(mail);
        }
        protected abstract Task SendEmailAsync(MailMessage mail);
        protected abstract void SendEmail(MailMessage mail);
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    Configuration.DefaultFromAddress,
                    Configuration.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
        #endregion
    }
}
