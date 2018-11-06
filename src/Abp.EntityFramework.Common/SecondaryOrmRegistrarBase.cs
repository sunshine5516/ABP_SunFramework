using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using AbpFramework.Orm;
using System;
using System.Reflection;
using AbpFramework.Reflection.Extensions;

namespace Abp.EntityFramework.Common
{
    public abstract class SecondaryOrmRegistrarBase : ISecondaryOrmRegistrar
    {
        #region 声明实例
        private readonly IDbContextEntityFinder _dbContextEntityFinder;
        private readonly Type _dbContextType;
        public abstract string OrmContextKey { get; }
        #endregion
        #region 构造函数
        protected SecondaryOrmRegistrarBase(Type dbContextType, IDbContextEntityFinder dbContextEntityFinder)
        {
            _dbContextType = dbContextType;
            _dbContextEntityFinder = dbContextEntityFinder;
        }
        #endregion
        #region 方法
        public virtual void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes)
        {
            AutoRepositoryTypesAttribute autoRepositoryAttr = _dbContextType.GetTypeInfo().GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>()
                                                              ?? defaultRepositoryTypes;

            foreach (EntityTypeInfo entityTypeInfo in _dbContextEntityFinder.GetEntityTypeInfos(_dbContextType))
            {
                Type primaryKeyType = EntityHelper.GetPrimaryKeyType(entityTypeInfo.EntityType);
                if (primaryKeyType == typeof(int))
                {
                    Type genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityTypeInfo.EntityType);
                    if (!iocManager.IsRegistered(genericRepositoryType))
                    {
                        Type implType = autoRepositoryAttr.RepositoryImplementation.GetTypeInfo().GetGenericArguments().Length == 1
                            ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityTypeInfo.EntityType)
                            : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType);

                        iocManager.Register(
                            genericRepositoryType,
                            implType,
                            DependencyLifeStyle.Transient
                        );
                    }
                }

                Type genericRepositoryTypeWithPrimaryKey = autoRepositoryAttr.RepositoryInterfaceWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType);
                if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey))
                {
                    Type implType = autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.GetTypeInfo().GetGenericArguments().Length == 2
                        ? autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType)
                        : autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType, primaryKeyType);

                    iocManager.Register(
                        genericRepositoryTypeWithPrimaryKey,
                        implType,
                        DependencyLifeStyle.Transient
                    );
                }
            }
        }
        #endregion

    }
}
