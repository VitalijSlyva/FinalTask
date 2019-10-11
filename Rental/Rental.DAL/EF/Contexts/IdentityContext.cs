using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        static IdentityContext()
        {
            Database.SetInitializer<IdentityContext>(new IdentityInitializer());
        }

        public IdentityContext() { }

        public IdentityContext(string connetion) : base(connetion)
        {

        }

        public DbSet<Profile> Profiles { get; set; }
    }
}
