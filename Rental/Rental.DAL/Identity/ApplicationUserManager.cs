using Microsoft.AspNet.Identity;
using Rental.DAL.Entities.Identity;

namespace Rental.DAL.Identity
{
    /// <summary>
    /// Standard mothods for working with user information.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Create connection to database.
        /// </summary>
        /// <param name="store">Database with users</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {

        }
    }
}
