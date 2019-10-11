using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class ReturnDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public CrashDM Crash { get; set; }

        public bool IsReturned { get; set; }

        public OrderDM Order { get; set; }
    }
}