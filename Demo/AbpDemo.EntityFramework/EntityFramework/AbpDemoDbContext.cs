using Abp.Zero.EntityFramework;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.MultiTenancy;
using System.Data.Common;
namespace AbpDemo.EntityFramework.EntityFramework
{
    public class AbpDemoDbContext: AbpZeroDbContext<Tenant, Role, User>
    {
        //public virtual IDbSet<Demo> Users { get; set; }
        public AbpDemoDbContext()
        : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ABPMultiMVCDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of AbpDemoDbContext since ABP automatically handles it.
         */
        public AbpDemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public AbpDemoDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public AbpDemoDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
