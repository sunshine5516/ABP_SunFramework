using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Zero.Authorization.Users
{
    public abstract class AbpUser<TUser> : AbpUserBase, IUser<long>, IFullAudited<TUser>
        where TUser : AbpUser<TUser>
    {
        public TUser CreatorUser { get; set; }
        public TUser LastModifierUser { get; set; }
       
        public TUser DeleterUser { get; set; }
        
    }
}
