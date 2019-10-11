using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class OrderDM : EntityDM
    {
        public Identity.ProfileDM Profile { set; get; }

        public CarDM Car { get; set; }

        public bool WithDriver { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public PaymentDM Payment { get; set; }
    }
}