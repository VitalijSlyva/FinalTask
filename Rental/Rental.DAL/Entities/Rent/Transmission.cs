using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Transmission entity.
    /// </summary>
    public class Transmission :Entity
    {
        public string Category { get; set; }

        public int Count { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
