using System.Net.Mail;
namespace AbpFramework.Net.Mail.Smtp
{
    public interface ISmtpEmailSender:IEmailSender
    {
        SmtpClient BuildClient();
    }
}
