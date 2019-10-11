using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class PaymentDM : EntityDM
    {
        public string TransactionId { get; set; }

        public bool IsPaid { get; set; }

        public int Price { get; set; }
    }
}