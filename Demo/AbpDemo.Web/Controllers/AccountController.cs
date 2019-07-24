using Abp.Zero.Authorization.Users;
using AbpDemo.Web.Models.Account;
using AbpFramework.Configuration.Startup;
using System.Threading.Tasks;
using System.Web.Mvc;
using AbpDemo.Core.MultiTenancy;
using AbpDemo.Core.Authorization.Users;
using Abp.Zero.Common.Authorization;
using System;
using AbpDemo.Core.Authorization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Abp.Web.Common.Web.Model;
using AbpFramework.UI;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Extensions;
using AbpFramework.Domain.Uow;
using AbpDemo.Application.Users;

namespace AbpDemo.Web.Controllers
{
    public class AccountController : ABPFrameworkDemoControllerBase
    {
        #region 声明实例
        //public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAuthenticationManager _authenticationManager;
        //private readonly ITenantCache _tenantCache;
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUserAppService _userAppService;
        #endregion
        #region 构造函数
        //static AccountController()
        //{
        //    OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        //}
        public AccountController(IMultiTenancyConfig multiTenancyConfig,
            LogInManager logInManager, UserManager userManager,
            IAuthenticationManager authenticationManager,
            IUnitOfWorkManager unitOfWorkManager, IUserAppService userAppService)
        {
            this._multiTenancyConfig = multiTenancyConfig;
            //this._tenantCache = tenantCache;
            this._logInManager = logInManager;
            this._userManager = userManager;
            this._authenticationManager = authenticationManager;
            this._unitOfWorkManager = unitOfWorkManager;
            this._userAppService = userAppService;
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
        #region 注册
        public ActionResult Register()
        {
            //_unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
            return RegisterView(new RegisterViewModel());
        }
        public ActionResult RegisterView(RegisterViewModel model)
        {
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            return View("Register", model);
        }
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                CheckModelState();
                var user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    EmailAddress = model.EmailAddress,
                    IsActive = true
                };
                ExternalLoginInfo externalLoginInfo = null;
                if(model.IsExternalLogin)
                {
                    externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
                    if(externalLoginInfo==null)
                    {
                        throw new ApplicationException("Can not external login!");
                    }
                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider=externalLoginInfo.Login.LoginProvider,
                            ProviderKey=externalLoginInfo.Login.ProviderKey
                        }
                    };
                    if (model.UserName.IsNullOrEmpty())
                    {
                        model.UserName = model.EmailAddress;
                    }
                    model.Password= Core.Authorization.Users.User.CreateRandomPassword();
                    if(string.Equals(externalLoginInfo.Email,model.EmailAddress,
                        StringComparison.InvariantCultureIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }
                }
                else
                {
                    //Username and Password are required if not external login
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException("FormIsNotValidMessage");
                    }
                }
                user.CreationTime = DateTime.Now;
                user.UserName = model.UserName;
                user.Password = new PasswordHasher().HashPassword(model.Password);
                _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
                //_logInManager.tempttt();                        
                CheckErrors(await _userManager.CreateAsync(user));
                await _unitOfWorkManager.Current.SaveChangesAsync();
                if(user.IsActive)
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if(externalLoginInfo!=null)
                    {
                        loginResult = await _logInManager.LoginAsync(externalLoginInfo.Login,
                            GetTenancyNameOrNull());
                    }
                    else
                    {
                        loginResult = await GetLoginResultAsync(user.UserName, model.Password
                            , GetTenancyNameOrNull());
                        //return Redirect(Url.Action("Index", "Home"));
                    }
                    if(loginResult.Result==AbpLoginResultType.Success)
                    {
                        await SignInAsync(loginResult.User, loginResult.Identity);
                        return Redirect(Url.Action("Index", "Home"));
                    }
                    Logger.Warn("New registered user could not be login. This should not be normally." +
                        " login result: " + loginResult.Result);
                }
                //If can not login, show a register result page
                return View("RegisterResult", new RegisterResultViewModel
                {
                    TenancyName=GetTenancyNameOrNull(),
                    NameAndSurname=user.Name+" "+user.Surname,
                    UserName=user.UserName,
                    EmailAddress=user.EmailAddress,
                    IsActive=user.IsActive
                });
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }
        #endregion
        #region 辅助方法
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
        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false;
            }
            return true;
        }
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return "Java";
        }
        /// <summary>
        /// 身份认证登录，验证用户凭据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private async Task SignInAsync(User user, ClaimsIdentity identity=null
            , bool rememberMe = false)
        {
            if(identity==null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent=rememberMe}, identity);
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
                    return new UserFriendlyException(("LoginFailed"), ("ThereIsNoTenantDefinedWithName{0}"+ tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(("LoginFailed"), ("TenantIsNotActive"+tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(("LoginFailed"), ("UserIsNotActiveAndCanNotLogin"+usernameOrEmailAddress));
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
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}