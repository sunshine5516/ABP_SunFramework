
using Abp.Dapper.Dapper;
using Abp.EntityFramework.EntityFramework;
using Abp.TestBase.TestBase;
using AbpFramework.Modules;
using DapperExtensions.Sql;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
namespace Abp.Dapper.Tests
{
    [DependsOn(
       typeof(AbpEntityFrameworkModule),
       typeof(AbpTestBaseModule),
       typeof(AbpDapperModule)
   )]
    public class AbpDapperTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsolationLevel = IsolationLevel.Unspecified;
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            DapperExtensions.DapperExtensions.SqlDialect = new SqliteDialect();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { Assembly.GetExecutingAssembly() });
        }
    }
}
