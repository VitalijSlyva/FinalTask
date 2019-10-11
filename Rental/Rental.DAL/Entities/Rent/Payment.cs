using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Payment:Entity
    {
        public string TransactionId { get; set; }

        public bool IsPaid { get; set; }

        public int Price { get; set; }

        public int? CrashId { get; set; }

        public virtual Crash Crash { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
