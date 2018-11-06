using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 封装了最后修改的用户的信息接口
    /// </summary>
    public interface IModificationAudited: IHasModificationTime
    {
        /// <summary>
        /// 最后修改的用户ID.
        /// </summary>
        long? LastModifierUserId { get; set; }
    }
    /// <summary>
    /// 封装了最后修改的用户的信息接口，泛型
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// 最后修改的用户
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}
