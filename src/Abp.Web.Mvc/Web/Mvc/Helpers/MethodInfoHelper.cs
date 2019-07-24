﻿using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abp.Web.Mvc.Web.Mvc.Helpers
{
    public static class MethodInfoHelper
    {
        public static bool IsJsonResult(MethodInfo method)
        {
            return typeof(JsonResult).IsAssignableFrom(method.ReturnType) ||
                typeof(Task<JsonResult>).IsAssignableFrom(method.ReturnType);
        }
    }
}
