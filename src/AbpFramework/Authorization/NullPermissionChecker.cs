using System.Threading.Tasks;
namespace AbpFramework.Authorization
{
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        public static NullPermissionChecker Instance { get; } = new NullPermissionChecker();
        public Task<bool> IsGrantedAsync(string permissionName)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            return Task.FromResult(true);
        }
    }
}
