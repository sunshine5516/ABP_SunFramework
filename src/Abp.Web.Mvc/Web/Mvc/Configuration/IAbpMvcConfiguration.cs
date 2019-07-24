using AbpFramework.Domain.Uow;
using AbpFramework.Web.Models;

namespace Abp.Web.Mvc.Web.Mvc.Configuration
{
    public interface IAbpMvcConfiguration
    {
        /// <summary>
        /// Default UnitOfWorkAttribute for all actions.
        /// </summary>
        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        /// <summary>
        /// Default WrapResultAttribute for all actions.
        /// </summary>
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        /// <summary>
        /// 默认: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// 默认: true.
        /// </summary>
        bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        /// <summary>
        /// MVC controllers审计是否可用.
        /// 默认: true.
        /// </summary>
        bool IsAuditingEnabled { get; set; }

        /// <summary>
        /// MVC actions审计是否可用.
        /// 默认: false.
        /// </summary>
        bool IsAuditingEnabledForChildActions { get; set; }
    }
}
