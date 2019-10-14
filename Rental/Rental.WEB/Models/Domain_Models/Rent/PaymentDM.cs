using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class PaymentDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер транзакции")]
        public string TransactionId { get; set; }

        [Display(Name = "Оплачено")]
        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Currency)]
        [Display(Name = "Сумма")]
        [Range(1, 10000000,ErrorMessage ="Неверное число")]
        public int Price { get; set; }
    }
}