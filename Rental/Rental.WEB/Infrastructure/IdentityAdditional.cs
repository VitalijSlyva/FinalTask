using Microsoft.AspNet.Identity;
using Rental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Infrastructure
{
    /// <summary>
    /// Additional methods for user identity.
    /// </summary>
    public static class IdentityAdditional
    {
        private static IAccountService _accountService;
        
        /// <summary>
        /// Get user login name.
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Name</returns>
        public static string GetUserName(this IIdentity identity)
        {
            _accountService =
              (IAccountService)DependencyResolver.Current.GetService(typeof(IAccountService));
            var userId=identity.GetUserId();
            string result = _accountService.GetUser(userId)?.Name??"";
            return result;
        }

        public static bool GetEmailConfirm(this IIdentity identity)
        {
            _accountService =
              (IAccountService)DependencyResolver.Current.GetService(typeof(IAccountService));
            var userId = identity.GetUserId();
            bool result = _accountService.GetUser(userId)?.ConfirmedEmail??false;
            return result;
        }
    }
}