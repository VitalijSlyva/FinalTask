using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class ReturnDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public CrashDM Crash { get; set; }

        [Display(Name = "Возвращено")]
        public bool IsReturned { get; set; }

        public OrderDM Order { get; set; }
    }
}