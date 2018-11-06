using AbpDemo.EntityFramework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.EntityFramework.Migrations.SeedData
{
    public class DefaultEditionsCreator
    {
        private readonly AbpDemoDbContext _context;
        public DefaultEditionsCreator(AbpDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            throw new NotImplementedException();
        }
    }
    
}
