using AbpFramework.Dependency;
using AbpFramework.Extensions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AbpFramework.Net.Mail.Smtp
{
    /// <summary>
    /// 用于通过SMTP发送电子邮件。
    /// 继承自EmailSenderBase，实现了ISmtpEmailSender接口。
    /// 该类就是基于SMTP协议进行邮件发送。提供了SendEmailAsync(MailMessage mail)
    /// 和SendEmail(MailMessage mail)，同步异步两种发送邮件的方法。
    /// </summary>
    public class SmtpEmailSender : EmailSenderBase, ISmtpEmailSender, ITransientDependency
    {
        #region 声明实例
        private readonly ISmtpEmailSenderConfiguration _configuration;
        #endregion
        #region 构造函数
        public SmtpEmailSender(ISmtpEmailSenderConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        #endregion
        #region 方法
        public SmtpClient BuildClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;
            var smtpClient= new SmtpClient(host, port);
            try
            {
                if(_configuration.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }
                if(_configuration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var userName = _configuration.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = _configuration.Password;
                        var domain = _configuration.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                            : new NetworkCredential(userName, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        protected override void SendEmail(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                smtpClient.Send(mail);
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }
        #endregion

    }
}
