using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Quality domain model.
    /// </summary>
    public class QualityDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Атрибут качества")]
        public string Text { get; set; }
    }
}