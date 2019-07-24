using System;
using System.Threading.Tasks;
using Abp.AutoMapper.AutoMapper;
using AbpDemo.Application.Sessions.Dto;
using AbpFramework.Auditing;
namespace AbpDemo.Application.Sessions
{
    public class SessionAppService : ABPMultiMVCAppServiceBase, ISessionAppService
    {
        #region 声明实例

        #endregion
        #region 构造函数

        #endregion
        #region 方法
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput();
            //var teme = await GetCurrentUserAsync();
            //output.User = (teme).MapTo<UserLoginInfoDto>();
            if (AbpSession.UserId.HasValue)
            {
                output.User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>();
            }
            //output.User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>();
            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }
            return output;
        }
        #endregion

    }
}
