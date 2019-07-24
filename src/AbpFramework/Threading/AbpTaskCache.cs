using System.Threading.Tasks;

namespace AbpFramework.Threading
{
    public static class AbpTaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
