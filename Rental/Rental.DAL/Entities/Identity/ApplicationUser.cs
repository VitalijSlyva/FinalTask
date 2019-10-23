using Microsoft.AspNet.Identity.EntityFramework;

namespace Rental.DAL.Entities.Identity
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public virtual Profile Profile { get; set; }

        public string Name { get; set; }
    }
}
