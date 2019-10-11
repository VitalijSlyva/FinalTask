using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Identity;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Services
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private IdentityContext _context;

        private IClientManager _clientManager;

        private ApplicationRoleManager _roleManager;

        private ApplicationUserManager _userManager;

        public IdentityUnitOfWork(string connectionString)
        {
            _context = new IdentityContext(connectionString);
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

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    RoleManager.Dispose();
                    RoleManager.Dispose();
                    ClientManager.Dispose();
                }
            }
            _Disposed = true;
        }
    }
}
