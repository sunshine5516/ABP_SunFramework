using AbpFramework.Configuration;
using System.Collections.Generic;
namespace AbpFramework.Net.Mail
{
    /// <summary>
    /// 定义发送电子邮件的设置
    /// </summary>
    internal class EmailSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            //return new[]
            //{
            //    new SettingDefinition(EmailSettingNames.Smtp.Host,"127.0.0.1","SmtpHost", scopes: SettingScopes.Application | SettingScopes.Tenant),
            //                           new SettingDefinition(EmailSettingNames.Smtp.Port, "25", ("SmtpPort"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.Smtp.UserName, "", ("Username"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.Smtp.Password, "", ("Password"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.Smtp.Domain, "", ("DomainName"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "false", ("UseSSL"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "true", ("UseDefaultCredentials"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.DefaultFromAddress, "", ("DefaultFromSenderEmailAddress"), scopes: SettingScopes.Application | SettingScopes.Tenant),
            //           new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "", ("DefaultFromSenderDisplayName"), scopes: SettingScopes.Application | SettingScopes.Tenant)
            //};
            return new[]
            {
                       new SettingDefinition(EmailSettingNames.Smtp.Host,"smtp.qq.com","SmtpHost", scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.Port, "25", ("SmtpPort"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.UserName, "961128502@qq.com", ("Username"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.Password, "qasvqhbggvrhbahg", ("Password"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.Domain, "", ("DomainName"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "True", ("UseSSL"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "false", ("UseDefaultCredentials"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.DefaultFromAddress, "961128502@qq.com", ("DefaultFromSenderEmailAddress"), scopes: SettingScopes.Application | SettingScopes.Tenant),
                       new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "961128502@qq.com", ("DefaultFromSenderDisplayName"), scopes: SettingScopes.Application | SettingScopes.Tenant)
            };
        }
    }
}
