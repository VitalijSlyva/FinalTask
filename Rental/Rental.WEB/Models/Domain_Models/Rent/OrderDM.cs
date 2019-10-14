using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class OrderDM : EntityDM
    {
        public Identity.ProfileDM Profile { set; get; }

        public CarDM Car { get; set; }

        [Display(Name = "С водителем")]
        public bool WithDriver { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Не является датой")]
        [Display(Name = "Дата оренды")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Не является датой")]
        [Display(Name = "Дата возврата")]
        public DateTime DateEnd { get; set; }

        public PaymentDM Payment { get; set; }
    }
}