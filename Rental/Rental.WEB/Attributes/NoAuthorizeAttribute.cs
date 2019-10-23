using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Attributes
{
    /// <summary>
    /// Attribute for only no authorize users
    /// </summary>
    public class NoAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Test authorization
        /// </summary>
        /// <param name="httpContext">Base context</param>
        /// <returns>Is not authorize</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !httpContext.User.Identity.IsAuthenticated;
        }
    }
}