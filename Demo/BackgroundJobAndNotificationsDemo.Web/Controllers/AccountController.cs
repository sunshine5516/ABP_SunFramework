using Abp.Web.Common.Web.Model;
using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization;
using AbpDemo.Core.Authorization;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Configuration.Startup;
using AbpFramework.UI;
using BackgroundJobAndNotificationsDemo.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BackgroundJobAndNotificationsDemo.Web.Controllers
{
    public class AccountController : BackgroundJobAndNotificationsDemoControllerBase
    {
        #region 声明实例
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        #endregion
        #region 构造函数
        public AccountController(IMultiTenancyConfig multiTenancyConfig,
            IAuthenticationManager authenticationManager,
            LogInManager logInManager, UserManager userManager)
        {
            this._multiTenancyConfig = multiTenancyConfig;
            this._authenticationManager = authenticationManager;
            this._logInManager = logInManager;
            this._userManager = userManager;
        }
        #endregion
        #region Login / Logout
        public ActionResult Login(string returnUrl = "")
        {
            //_userAppService.GetAll();
            //UnitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                    IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                    MultiTenancySide = AbpSession.MultiTenancySide
                });
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel,
            string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();
            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                GetTenancyNameOrNull());
            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }
            //return Task.FromResult(Redirect("Home/index"));
            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        #endregion
        #region 方法
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return "Java";
        }

        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取登录结果
        /// </summary>
        /// <param name="usernameOrEmailAddress">用户名或电子邮件</param>
        /// <param name="password">密码</param>
        /// <param name="tenancyName">租户</param>
        /// <returns></returns>
        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync
            (string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success://登录成功
                    return loginResult;
                default://登录异常处理
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }
        /// <summary>
        /// 身份认证登录，验证用户凭据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private async Task SignInAsync(User user, ClaimsIdentity identity = null
            , bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }
        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(("LoginFailed"), ("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(("LoginFailed"), ("ThereIsNoTenantDefinedWithName{0}" + tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(("LoginFailed"), ("TenantIsNotActive" + tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(("LoginFailed"), ("UserIsNotActiveAndCanNotLogin" + usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(("LoginFailed"), ("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(("LoginFailed"));
            }
        }
        #endregion
    }
}