using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Interfaces;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Manager for work with profile.
    /// </summary>
    public class ClientManager : IClientManager
    {
        private IdentityContext _context;

        /// <summary>
        /// Create connection with database.
        /// </summary>
        /// <param name="context">Identity context</param>
        public ClientManager(IdentityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create profile.
        /// </summary>
        /// <param name="profile">New profile.</param>
        public void Create(Profile profile)
        {
            _context.Entry(profile).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();
        }

        /// <summary>
        /// Dispose context.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Update profile.
        /// </summary>
        /// <param name="profile">Updated profile.</param>
        public void Update(Profile profile)
        {
            _context.Entry(profile).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
