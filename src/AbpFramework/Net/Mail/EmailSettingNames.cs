namespace AbpFramework.Net.Mail
{
    /// <summary>
    /// 静态常量类，主要定义了发送邮件需要的相关参数：Port、Host、
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// 默认地址
        /// </summary>
        public const string DefaultFromAddress = "Abp.Net.Mail.DefaultFromAddress";
        /// <summary>
        /// 默认名称
        /// </summary>
        public const string DefaultFromDisplayName = "Abp.Net.Mail.DefaultFromDisplayName";
        /// <summary>
        /// SMTP相关电子邮件设置
        /// </summary>
        public static class Smtp
        {
            public const string Host = "Abp.Net.Mail.Smtp.Host";
            public const string Port= "Abp.Net.Mail.Smtp.Port";
            public const string UserName = "Abp.Net.Mail.Smtp.UserName";
            public const string Password= "Abp.Net.Mail.Smtp.Password";
            public const string Domain = "Abp.Net.Mail.Smtp.Domain";
            public const string EnableSsl = "Abp.Net.Mail.Smtp.EnableSsl";
            public const string UseDefaultCredentials = "Abp.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}
