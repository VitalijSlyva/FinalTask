using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Crash entity.
    /// </summary>
    public class Crash :Entity
    {
        public int? ReturnId { get; set; }

        public virtual Return Return { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
    }
}
