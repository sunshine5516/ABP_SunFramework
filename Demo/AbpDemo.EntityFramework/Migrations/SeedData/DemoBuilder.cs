using AbpDemo.Core;
using AbpDemo.EntityFramework.EntityFramework;
using System.Linq;

namespace AbpDemo.EntityFramework.Migrations.SeedData
{
    public class DemoBuilder
    {
        private readonly AbpDemoDbContext _context;
        private readonly int _tenantId;
        public DemoBuilder(AbpDemoDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }
        public void Create()
        {
            //CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId);
            if (adminUser == null)
            {
                //adminUser = Demo.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com", "123");
                //adminUser.IsEmailConfirmed = true;
                //adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();               
                _context.SaveChanges();
            }
        }
    }
}
