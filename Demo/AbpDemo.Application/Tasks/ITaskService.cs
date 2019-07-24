using System.Threading.Tasks;

namespace AbpDemo.Application.Tasks
{
    public interface ITaskService
    {
        void Test();
        void TestBackgroundJobs();
        void Send();
    }
}
