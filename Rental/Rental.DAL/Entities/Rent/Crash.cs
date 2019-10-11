using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Crash :Entity
    {
        public int? ReturnId { get; set; }

        public virtual Return Return { get; set; }

        public string Description { get; set; }

        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
