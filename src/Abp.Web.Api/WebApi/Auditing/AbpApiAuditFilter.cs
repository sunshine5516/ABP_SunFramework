using Abp.WebApi.Validation;
using AbpFramework.Auditing;
using AbpFramework.Dependency;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Abp.WebApi.Auditing
{
    public class AbpApiAuditFilter : IActionFilter, ITransientDependency
    {
        #region 声明实例
        public bool AllowMultiple =>false;
        private readonly IAuditingHelper _auditingHelper;
        #endregion
        #region 构造函数
        public AbpApiAuditFilter(IAuditingHelper auditingHelper)
        {
            this._auditingHelper = auditingHelper;
        }
        #endregion
        #region 方法
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var method=actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if(method==null||!ShouldSaveAudit(actionContext))
            {
                return await continuation();
            }
            var auditInfo = _auditingHelper.CreateAuditInfo(
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerType,
                method,actionContext.ActionArguments);
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return await continuation();
            }
            catch(Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                await _auditingHelper.SaveAsync(auditInfo);
            }
        }

        private bool ShouldSaveAudit(HttpActionContext context)
        {
            if (context.ActionDescriptor.IsDynamicAbpAction())
            {
                return false;
            }

            return _auditingHelper.ShouldSaveAudit(context.ActionDescriptor.GetMethodInfoOrNull(), true);
        }
        #endregion


    }
}
