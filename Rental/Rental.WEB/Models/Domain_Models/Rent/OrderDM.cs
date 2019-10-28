using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Order domain model.
    /// </summary>
    public class OrderDM : EntityDM
    {
        public Identity.ProfileDM Profile { set; get; }

        public CarDM Car { get; set; }

        [Display(Name = "С водителем (300грн. сутки)")]
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