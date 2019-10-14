using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class EntityDM
    {
        [Display(Name = "Идентификационный номер")]
        public int Id { get; set; }
    }
}