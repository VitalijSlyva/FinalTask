using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Crash domain model.
    /// </summary>
    public class CrashDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public PaymentDM Payment { get; set; }
    }
}