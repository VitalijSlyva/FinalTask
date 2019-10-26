using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Identity;
using System.Data.Entity;

namespace Rental.DAL.EF.Initializers
{
    /// <summary>
    /// Initializator for identity context.
    /// </summary>
    internal class IdentityInitializer : CreateDatabaseIfNotExists<IdentityContext>
    {
        /// <summary>
        /// Recreate database with new items.
        /// </summary>
        /// <param name="context">Database context</param>
        protected override void Seed(IdentityContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            roleManager.Create(role1);

            var admin = new ApplicationUser
            {
                UserName = "adminEmail@gmail.com",
                Email = "adminEmail@gmail.com",
                Name = "ADMIN",
                EmailConfirmed = true
            };
            string password = "P@ssw0rd";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }
            base.Seed(context);
        }
    }
}
