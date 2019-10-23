using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Identity;
using System.Data.Entity;

namespace Rental.DAL.EF.Contexts
{
    /// <summary>
    /// Context for identity entities.
    /// </summary>
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Create initializator for database.
        /// </summary>
        static IdentityContext()
        {
            Database.SetInitializer<IdentityContext>(new IdentityInitializer());
        }

        /// <summary>
        /// Default builder.
        /// </summary>
        public IdentityContext()
        {

        }

        /// <summary>
        /// Connect to database.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public IdentityContext(string connetion) : base(connetion)
        {

        }

        /// <summary>
        /// Profile table.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }
    }
}
