namespace AbpFramework.Net.Mail
{
    /// <summary>
    /// 电子邮件配置接口
    /// </summary>
    public interface IEmailSenderConfiguration
    {
        string DefaultFromAddress { get; }
        string DefaultFromDisplayName { get; }
    }
}
