using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Quality entity.
    /// </summary>
    public class Quality:Entity
    {
        public string Text { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
