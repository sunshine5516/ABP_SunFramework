using System.Collections.Generic;
using System.Threading.Tasks;
namespace AbpFramework.Application.Navigation
{
    public interface IUserNavigationManager
    {
        /// <summary>
        /// 获取专门为给定用户提供的菜单。.
        /// </summary>
        /// <param name="menuName">唯一名称</param>
        /// <param name="user">The user, or null for anonymous users</param>
        //Task<UserMenu> GetMenuAsync(string menuName, UserIdentifier user);
        Task<UserMenu> GetMenuAsync(string menuName, UserIdentifier user);
        /// <summary>
        /// 获取指定用户的所有菜单选项
        /// </summary>
        /// <param name="user">User id or null for anonymous users</param>
        //Task<IReadOnlyList<UserMenu>> GetMenusAsync(UserIdentifier user);
        Task<IReadOnlyList<UserMenu>> GetMenusAsync(UserIdentifier user);
    }
}
