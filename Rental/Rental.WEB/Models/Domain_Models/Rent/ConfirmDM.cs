using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class ConfirmDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public OrderDM Order { get; set; }

        [Display(Name = "Принято")]
        public bool IsConfirmed { get; set; }

        [Display(Name ="Коментарий")]
        public string Description { get; set; }
    }
}