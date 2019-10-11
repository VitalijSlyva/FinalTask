using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Services
{
    public class ClientManager : IClientManager
    {
        private IdentityContext _context;

        public ClientManager(IdentityContext context)
        {
            _context = context;
        }

        public void Create(Profile profile)
        {
            _context.Entry(profile).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Update(Profile profile)
        {
            _context.Entry(profile).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
