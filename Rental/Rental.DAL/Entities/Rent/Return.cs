using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Return entity. 
    /// </summary>
    public class Return:Entity
    { 
        public string ManagerId { get; set; }

        public virtual ICollection<Crash> Crash { get; set; }

        public bool IsReturned { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
