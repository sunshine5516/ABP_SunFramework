using Abp.EntityFramework.Common;
using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using AbpFramework.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
namespace Abp.EntityFramework.EntityFramework
{
    public class EfDbContextEntityFinder : IDbContextEntityFinder, ITransientDependency
    {
        public IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            //var t1= from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //        where
            //        (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
            //         ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
            //        ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
            //        select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType); ;
            //var t2 = dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                 ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }
    }
}
