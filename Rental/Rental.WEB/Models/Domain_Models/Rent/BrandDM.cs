using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class BrandDM:EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name="Марка")]
        public string Name { get; set; }
    }
}