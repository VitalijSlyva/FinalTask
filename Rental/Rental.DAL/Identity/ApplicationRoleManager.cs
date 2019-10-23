using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.Entities.Identity;

namespace Rental.DAL.Identity
{
    /// <summary>
    /// Standard methods for working with role.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        /// <summary>
        /// Create connection to database.
        /// </summary>
        /// <param name="store">Database with roles</param>
        public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store)
        {

        }
    }
}
