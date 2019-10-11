using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Order:Entity
    {
        public string ClientId { set; get; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        public bool WithDriver { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }

        public int? ReturnId { get; set; }

        public virtual Return Return { get; set; }

        public int? ConfirmId { get; set; }

        public virtual Confirm Confirm { get; set; }
    }
}
