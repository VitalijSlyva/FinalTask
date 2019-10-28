using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Identity;
using Rental.DAL.Interfaces;
using System;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Unit for work with identity.
    /// </summary>
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private IdentityContext _context;

        private IClientManager _clientManager;

        private ApplicationRoleManager _roleManager;

        private ApplicationUserManager _userManager;

        /// <summary>
        /// Create context with connection.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public IdentityUnitOfWork(string connection)
        {
            _context = new IdentityContext(connection);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                    _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
                return _roleManager;
            }
        }

        public IClientManager ClientManager
        {
            get
            {
                if (_clientManager == null)
                    _clientManager = new ClientManager(_context);
                return _clientManager;
            }
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
