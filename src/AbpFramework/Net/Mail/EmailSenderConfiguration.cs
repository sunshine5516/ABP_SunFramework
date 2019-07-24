using AbpFramework.Configuration;
using AbpFramework.Extensions;
namespace AbpFramework.Net.Mail
{
    public class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        #region 声明实例
        public virtual string DefaultFromAddress
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.DefaultFromAddress); }
        }

        public string DefaultFromDisplayName
        {
            get { return SettingManager.GetSettingValue(EmailSettingNames.DefaultFromDisplayName); }
        }
        protected readonly ISettingManager SettingManager;
        #endregion
        #region 构造函数
        protected EmailSenderConfiguration(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }
        #endregion
        protected string GetNotEmptySettingValue(string name)
        {
            var value = SettingManager.GetSettingValue(name);
            if(value.IsNullOrEmpty())
            {
                throw new AbpException($"Setting value for '{name}' is null or empty!");
            }
            return value;
        }
    }
}
