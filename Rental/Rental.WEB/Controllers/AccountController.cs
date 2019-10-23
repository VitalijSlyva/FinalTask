using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Rental.BLL.DTO.Identity;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.View_Models.Account;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    /// <summary>
    /// Controller for account actions.
    /// </summary>
    [ExceptionLogger]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        private IIdentityMapperDM _identityMapperDM;

        private ILogWriter _logWriter;

        private IAuthenticationManager _authenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Create services and mappers for work.
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="identityMapperDM">Identity mapper</param>
        /// <param name="log">Log service</param>
        public AccountController(IAccountService accountService, IIdentityMapperDM identityMapperDM,ILogWriter log)
        {
            _accountService = accountService;
            _identityMapperDM = identityMapperDM;
            _logWriter = log;
        }

        /// <summary>
        /// Show login view.
        /// </summary>
        /// <returns>View</returns>
        [NoAuthorize]
        public ActionResult Login()
        {
            return View("Login");
        }

        /// <summary>
        /// Show register view.
        /// </summary>
        /// <returns>View</returns>
        [NoAuthorize]
        public ActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="login">Login model</param>
        /// <returns>View</returns>
        [NoAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = login.Email, Password = login.Password };
                ClaimsIdentity claim = await _accountService.AuthenticateAsync(user);
                if (claim == null)
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                else
                {
                    _authenticationManager.SignOut();
                    _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    return RedirectToAction("Index", "Rent");
                }
            }
            return View(login);
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>View</returns>
        [Authorize]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Rent");
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="register">Register model</param>
        /// <returns>View</returns>
        [NoAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = register.Email,
                    Password = register.Password,
                    Name = register.Name,
                };
                string result= await _accountService.CreateAsync(user);
                if (result!=null&&result.Length == 0)
                    return await Login(new LoginVM() { Email = register.Email, Password = register.Password });
                else
                    ModelState.AddModelError("",result);
            }
            return View("Register",register);
        }

        /// <summary>
        /// Show user data.
        /// </summary>
        /// <returns>View</returns>
        [Authorize]
        public async Task<ActionResult> ShowUser()
        {
            string id = User.Identity.GetUserId();
            var user = await _accountService.GetUserAsync(id);
            UserDM userProfile = _identityMapperDM.ToUserDM.Map<User, UserDM>(user);
            return View("ShowUser",userProfile);
        }

        /// <summary>
        /// Dispose services.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}