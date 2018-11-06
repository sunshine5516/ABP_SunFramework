using AbpFramework.Dependency;
using AbpFramework.Runtime;
using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;

namespace Abp.EntityFramework.EntityFramework.Interceptors
{
    /// <summary>
    /// SQL命令拦截器
    /// </summary>
    public class WithNoLockInterceptor: DbCommandInterceptor, ITransientDependency
    {
        #region 声明实例
        private const string InterceptionContextKey = "Abp.EntityFramework.Interceptors.WithNolockInterceptor";
        private static readonly Regex TableAliasRegex = new Regex(@"(?<tableAlias>AS \[Extent\d+\](?! WITH \(NOLOCK\)))", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private readonly IAmbientScopeProvider<InterceptionContext> _interceptionScopeProvider;
        public InterceptionContext NolockingContext => _interceptionScopeProvider.GetValue(InterceptionContextKey);

        #endregion
        #region 构造函数
        public WithNoLockInterceptor(IAmbientScopeProvider<InterceptionContext> interceptionScopeProvider)
        {
            _interceptionScopeProvider = interceptionScopeProvider;
        }
        #endregion

        #region 方法
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (NolockingContext?.UseNolocking ?? false)
            {
                command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
                NolockingContext.CommandText = command.CommandText;
            }
        }
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (NolockingContext?.UseNolocking ?? false)
            {
                command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
                NolockingContext.CommandText = command.CommandText;
            }
        }
        public IDisposable UseNolocking()
        {
            return _interceptionScopeProvider.BeginScope(InterceptionContextKey, new InterceptionContext(string.Empty, true));
        }
        #endregion

        public class InterceptionContext
        {
            public string CommandText { get; set; }

            public bool UseNolocking { get; set; }
            public InterceptionContext(string commandText, bool useNolocking)
            {
                CommandText = commandText;
                UseNolocking = useNolocking;
            }
        }
    }
}
