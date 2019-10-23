using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Transmission domain model.
    /// </summary>
    public class TransmissionDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Тип коробки")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество передач")]
        [Range(1,1000, ErrorMessage = "Неверное число")]
        public int Count { get; set; }
    }
}