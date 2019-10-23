using System;
using System.Collections.Generic;

namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Order entity.
    /// </summary>
    public class Order:Entity
    {
        public string ClientId { set; get; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        public bool WithDriver { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }

        public virtual ICollection<Return> Return { get; set; }

        public virtual ICollection<Confirm> Confirm { get; set; }
    }
}
