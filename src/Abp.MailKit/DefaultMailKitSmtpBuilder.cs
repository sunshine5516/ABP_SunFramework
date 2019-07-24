using AbpFramework.Dependency;
using AbpFramework.Net.Mail.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;

namespace Abp.MailKit
{
    public class DefaultMailKitSmtpBuilder : IMailKitSmtpBuilder, ITransientDependency
    {
        #region 声明实例
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        #endregion
        #region 构造函数
        public DefaultMailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration)
        {
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
        }
        #endregion
        #region 方法
        public SmtpClient Build()
        {
            var client = new SmtpClient();
            try
            {
                ConfigureClient(client);
                return client;
            }
            catch(Exception e)
            {
                client.Dispose();
                throw e;
            }
        }

        protected virtual void ConfigureClient(SmtpClient client)
        {
            client.Connect
                (_smtpEmailSenderConfiguration.Host,
                _smtpEmailSenderConfiguration.Port,
                //_smtpEmailSenderConfiguration.EnableSsl
                SecureSocketOptions.StartTls
                );
            if (_smtpEmailSenderConfiguration.UseDefaultCredentials)
            {
                return;
            }
            client.Authenticate(
                _smtpEmailSenderConfiguration.UserName,
                _smtpEmailSenderConfiguration.Password
            );
        }
        #endregion

    }
}
