using AbpFramework.Collections.Extensions;
using Microsoft.AspNet.Identity;
using System;

namespace Abp.Zero.IdentityFramework
{
    public static class IdentityResultExtensions
    {
        public static void CheckErrors(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return;
            }

            throw new Exception(identityResult.Errors.JoinAsString(", "));
        }
    }
}
