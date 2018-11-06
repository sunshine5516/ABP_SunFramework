using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.Application.Users
{
    public interface IUserAppService:IApplicationService
    {
        List<User> GetAll();
    }
}
