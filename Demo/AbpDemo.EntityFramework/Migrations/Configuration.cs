using global::EntityFramework.DynamicFilters;
using System.Data.Entity.Migrations;

namespace AbpDemo.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EntityFramework.AbpDemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EntityFramework.AbpDemoDbContext context)
        {
            context.DisableAllFilters();
            //new DemoBuilder(context, 1).Create();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
