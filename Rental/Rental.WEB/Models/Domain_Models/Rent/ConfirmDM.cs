using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class ConfirmDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public OrderDM Order { get; set; }

        public bool IsConfirmed { get; set; }

        public string Description { get; set; }
    }
}