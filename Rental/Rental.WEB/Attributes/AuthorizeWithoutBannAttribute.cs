using Microsoft.AspNet.Identity;
using Rental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Attributes
{
    public class AuthorizeWithoutBannAttribute:AuthorizeAttribute
    {
        private IAccountService _accountService= (IAccountService)DependencyResolver.Current.GetService(typeof(IAccountService));

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated&&
                !_accountService.IsBanned(httpContext.User.Identity.GetUserId());
        }
    }
}