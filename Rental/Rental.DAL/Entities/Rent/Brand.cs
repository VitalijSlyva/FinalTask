using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Brand entity.
    /// </summary>
    public class Brand :Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
