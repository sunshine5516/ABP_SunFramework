using Abp.EntityFramework.Common;
using AbpFramework;
using System;
namespace Abp.EntityFramework.EntityFramework
{
    public class EfBasedSecondaryOrmRegistrar : SecondaryOrmRegistrarBase
    {
        #region 声明实例

        #endregion
        #region 构造函数
        public EfBasedSecondaryOrmRegistrar(Type dbContextType, IDbContextEntityFinder dbContextEntityFinder)
            : base(dbContextType, dbContextEntityFinder)
        {
           
        }
        #endregion
        #region 方法

        #endregion
        public override string OrmContextKey => AbpConsts.Orms.EntityFramework;
    }
}
