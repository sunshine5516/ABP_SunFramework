using Abp.MultiTenancy;
using System.Threading.Tasks;

namespace Abp.Zero.Common.Authorization.Users
{
    /// <summary>
    /// 定义外部授权源
    /// </summary>
    public interface IExternalAuthenticationSource<TTenant, TUser>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUserBase
    {
        /// <summary>
        /// 资源唯一名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 用于尝试通过此源对用户进行身份验证。
        /// </summary>
        /// <param name="userNameOrEmailAddress">用户名或邮件</param>
        /// <param name="plainPassword">密码</param>
        /// <param name="tenant">用户的租户 (if user is a host user)</param>
        /// <returns>True, 表示此使用已通过此源验证</returns>
        Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, TTenant tenant);
        /// <summary>
        /// 此方法是由此源进行身份验证的用户，该用户尚不存在。因此，source应创建User和填充属性。
        /// </summary>
        /// <param name="userNameOrEmailAddress">用户名或邮件</param>
        /// <param name="tenant">用户的租户(if user is a host user)</param>
        /// <returns>新建的用户</returns>
        Task<TUser> CreateUserAsync(string userNameOrEmailAddress, TTenant tenant);
        /// <summary>
        /// 更新用户；在此源对现有用户进行身份验证后调用此方法。它可用于更新源的某些用户属性。
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="tenant">用户的租户 (if user is a host user)</param>
        Task UpdateUserAsync(TUser user, TTenant tenant);
    }
}
