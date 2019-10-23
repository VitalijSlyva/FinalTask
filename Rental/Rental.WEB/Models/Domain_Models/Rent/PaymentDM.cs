using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Payment domain model.
    /// </summary>
    public class PaymentDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер транзакции")]
        public string TransactionId { get; set; }

        [Display(Name = "Оплачено")]
        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Сумма")]
        [Range(1, 10000000,ErrorMessage ="Неверное число")]
        public int Price { get; set; }
    }
}