using AbpFramework.Aspects;
using AbpFramework.Threading;
using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
namespace AbpFramework.Auditing
{
    public class AuditingInterceptor : IInterceptor
    {
        private readonly IAuditingHelper _auditingHelper;
        public AuditingInterceptor(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }
        public void Intercept(IInvocation invocation)
        {
            //判断过滤器是否已经处理了过了
            if (AbpCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, AbpCrossCuttingConcerns.Auditing))
            {
                invocation.Proceed();
                return;
            }
            //通过 IAuditingHelper 来判断当前方法是否需要记录审计日志信息
            if (!_auditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }
            //构造审计信息
            var auditInfo = _auditingHelper.CreateAuditInfo(invocation.TargetType, invocation.MethodInvocationTarget, invocation.Arguments);
            //判断方法的类型，同步方法与异步方法的处理逻辑不一样
            if (invocation.Method.IsAsync())
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }
        }
        /// <summary>
        /// 同步方法的处理逻辑与 MVC 过滤器逻辑相似
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="auditInfo"></param>
        private void PerformSyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                _auditingHelper.Save(auditInfo);
            }
        }
        /// <summary>
        /// 异步方法处理
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="auditInfo"></param>
        private void PerformAsyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();
            invocation.Proceed();
            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
            else
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
        }
        private void SaveAuditInfo(AuditInfo auditInfo, Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();
            auditInfo.Exception = exception;
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            _auditingHelper.Save(auditInfo);
        }
    }
}
