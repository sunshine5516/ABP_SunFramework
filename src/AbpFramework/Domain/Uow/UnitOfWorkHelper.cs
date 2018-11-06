using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// UOW帮助类
    /// </summary>
    public static class UnitOfWorkHelper
    {
        /// <summary>
        /// 是否具有UnitOfWorkAttribute属性.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static bool HasUnitOfWorkAttribute(MemberInfo memberInfo)
        {
            return memberInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}
