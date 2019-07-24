using Abp.TestBase.TestBase;
using AbpFramework.Configuration.Startup;
using Castle.MicroKernel.Registration;
using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
namespace Abp.Dapper.Tests
{
    public abstract class DapperApplicationTestBase : AbpIntegratedTestBase<AbpDapperTestModule>
    {
        protected DapperApplicationTestBase()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            Resolve<IAbpStartupConfiguration>().DefaultNameOrConnectionString = "Data Source=:memory:";
            AbpSession.UserId = 1;
            AbpSession.TenantId = 1;
        }
        protected override void PreInitialize()
        {
            base.PreInitialize();
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                .UsingFactoryMethod(() =>
                {
                    var connection = new SQLiteConnection(Resolve<IAbpStartupConfiguration>().DefaultNameOrConnectionString);
                    connection.Open();
                    var files = new List<string>
                    {
                        ReadScriptFile("CreateInitialTables")
                    };
                    foreach (string setupFile in files)
                    {
                        connection.Execute(setupFile);
                    }
                    return connection;
                }).LifestyleSingleton());
        }
        private string ReadScriptFile(string name)
        {
            string fileName = GetType().Namespace + ".Scripts" + "." + name + ".sql";
            using (Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
            {
                if(resource!=null)
                {
                    using (var sr = new StreamReader(resource))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }
    }
}
