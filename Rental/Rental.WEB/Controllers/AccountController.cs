using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.View_Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        private IIdentityMapperDM _identityMapperDM;

        private ILogService _logService;

        private IAuthenticationManager _authenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IAccountService accountService, IIdentityMapperDM identityMapperDM,ILogService logService)
        {
            _accountService = accountService;
            _identityMapperDM = identityMapperDM;
            _logService = logService;
        }

        public void CreateLog(string action,string authorId)
        {
            ActionLogDTO log = new ActionLogDTO()
            {
                Action=action,
                Time=DateTime.Now,
                AuthorId=authorId
            };
            _logService.CreateActionLog(log);
        }

        [ExceptionLogger]
        [NoAuthorize]
        public ActionResult Login()
        {
            return View();
        }

        [ExceptionLogger]
        [NoAuthorize]
        public ActionResult Register()
        {
            return View();
        }

        [ExceptionLogger]
        [NoAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM login)
        {
            if (login.Email == null || login.Password == null)
                ModelState.AddModelError("", "Неверный логин или пароль.");
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
                    CreateLog("Вошел", User.Identity.GetUserId());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }

        [ExceptionLogger]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            CreateLog("Вышел", User.Identity.GetUserId());
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ExceptionLogger]
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
                await _accountService.CreateAsync(user);
                return await Login(new LoginVM() { Email = register.Email, Password = register.Password });
            }
            return View(register);
        }

        [ExceptionLogger]
        [Authorize]
        public async Task<ActionResult> ShowUser()
        {
            string id = User.Identity.GetUserId();
            var user = await _accountService.GetUserAsync(id);
            UserDM userProfile = _identityMapperDM.ToUserDM.Map<User, UserDM>(user);
            return View(userProfile);
        }

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}