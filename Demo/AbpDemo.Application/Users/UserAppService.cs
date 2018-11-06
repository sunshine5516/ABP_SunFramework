using System.Collections.Generic;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Domain.Repositories;
namespace AbpDemo.Application.Users
{
    public class UserAppService: IUserAppService
    {
        #region 声明实例
        private readonly IRepository<User, long> _userRepository;

        public List<User> GetAll()
        {
           return _userRepository.GetAllList();
        }
        #endregion
        #region 构造函数
        public UserAppService(IRepository<User, long> userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion
        #region 方法

        #endregion
    }
}
