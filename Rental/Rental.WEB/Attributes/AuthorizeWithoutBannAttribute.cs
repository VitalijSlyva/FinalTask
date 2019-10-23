using Microsoft.AspNet.Identity;
using Rental.BLL.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Attributes
{
    /// <summary>
    /// Test that user was banned
    /// </summary>
    public class AuthorizeWithoutBannAttribute:AuthorizeAttribute
    {
        private IAccountService _accountService= 
            (IAccountService)DependencyResolver.Current.GetService(typeof(IAccountService));

        /// <summary>
        /// Test on ban
        /// </summary>
        /// <param name="httpContext">Http context</param>
        /// <returns>Is not ban</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated&&
                !_accountService.IsBanned(httpContext.User.Identity.GetUserId());
        }
    }
}