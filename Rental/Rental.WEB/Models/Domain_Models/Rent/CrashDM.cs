using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class CrashDM : EntityDM
    {
        public string Description { get; set; }

        public PaymentDM Payment { get; set; }
    }
}