using JetBrains.Annotations;
using System.Linq;
namespace AbpFramework.RealTime
{
    public static class OnlineClientManagerExtensions
    {
        public static bool IsOnline([NotNull] this IOnlineClientManager onlineClientManager,
            [NotNull] UserIdentifier user)
        {
            return onlineClientManager.GetAllByUserId(user).Any();
        }
    }
}
