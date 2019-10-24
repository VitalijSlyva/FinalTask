using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Rental.BLL.DTO.Identity;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.View_Models.Account;
using System.Net.Mail;
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
                if (result != null && result.Length == 0)
                {
                    _sendEmail(register.Email);
                    return await Login(new LoginVM() { Email = register.Email, Password = register.Password });
                }
                else
                    ModelState.AddModelError("", result);
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

        private void _sendEmail(string to)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("cardoorrental@gmail.com", "Cardoor");
            mailMessage.To.Add(to);
            mailMessage.Subject = "Подтверждение почты";
            mailMessage.Body= string.Format("Для завершения регистрации перейдите по ссылке:" +
                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                Url.Action("ConfirmEmail", "Account", 
                new { token = _accountService.GetIdByEmail(to) , email = to }, Request.Url.Scheme));
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }

        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _accountService.GetUserAsync(token);
            if (user != null)
            {
                if (user.Email == email&&!user.ConfirmedEmail)
                {
                    _accountService.ConfirmEmail(user.Id);
                    return RedirectToAction("Index", "Rent");
                }
                else
                {
                    return View("CustomNotFound", "_Layout", "Страница не доступна");
                }
            }
            else
            {
                return View("CustomNotFound", "_Layout", "Страница не доступна");
            }
        }

        public ActionResult ChangeName()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeName(ChangeNameVM model)
        {
            if (ModelState.IsValid)
            {
               string res= _accountService.ChangeName(model.Name, User.Identity.GetUserId(), model.Password);
                if (res.Length == 0)
                {
                    return RedirectToAction("Logout");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }
            }
            return View(model);
        }

        public ActionResult ChangeEmail()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailVM model)
        {
            if (ModelState.IsValid)
            {
                string res = _accountService.ChangeEmail(model.Email, User.Identity.GetUserId(), model.Password);
                if (res.Length == 0)
                {
                    return RedirectToAction("Logout");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }
            }
            return View(model);
        }

        public ActionResult ResetPasswordEmail(string email)
        {
            string id = null;
            if (!string.IsNullOrEmpty(email))
                id = _accountService.GetIdByEmail(email);
            else
            {
                id = User.Identity.GetUserId();
                email = User.Identity.Name;
            }
            _sendEmailResetPassword(email, id);
            return View("ResetPassowrdEmail");
        }

        private void _sendEmailResetPassword(string to,string id)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("cardoorrental@gmail.com", "Cardoor");
            mailMessage.To.Add(to);
            mailMessage.Subject = "Измененение пароля";
            mailMessage.Body = string.Format("Для изменения пароля по ссылке:" +
                            "<a href=\"{0}\" title=\"Изменить пароль\">{0}</a>",
                Url.Action("ChangePassword", "Account",
                new { token = id, email = to }, Request.Url.Scheme));
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }

        public ActionResult ChangePassword(string token, string email)
        {
            var user =  _accountService.GetUser(token);
            if (user != null)
            {
                if (user.Email == email )
                {
                    return View(new ChangePasswordVM() { Id=user.Id});
                }
                else
                {
                    return View("CustomNotFound", "_Layout", "Страница не доступна");
                }
            }
            else
            {
                return View("CustomNotFound", "_Layout", "Страница не доступна");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                string res = _accountService.ChangePassword(model.Id, model.Password);
                if (res.Length == 0)
                {
                    return RedirectToAction("Logout");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }
            }
            return View(model);
        }
    }
}