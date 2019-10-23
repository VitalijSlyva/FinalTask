using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Carcass entity.
    /// </summary>
    public class Carcass :Entity
    {
        public string Type { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
