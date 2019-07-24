namespace AbpFramework.Net.Mail.Smtp
{
    /// <summary>
    /// 定义由SmtpClient对象使用的配置
    /// </summary>
    public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// SMTP主机.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// SMTP端口.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// 登录到SMTP服务器的用户名.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 登录到SMTP服务器密码.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// 登录到SMTP服务器的域名。
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// 是否启用SSL?
        /// </summary>
        bool EnableSsl { get; }

        /// <summary>
        /// 使用默认凭据?
        /// </summary>
        bool UseDefaultCredentials { get; }
    }
}
