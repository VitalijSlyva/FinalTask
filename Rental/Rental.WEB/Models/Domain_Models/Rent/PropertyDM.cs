using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Property domain model.
    /// </summary>
    public class PropertyDM:EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Параметр")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Значение")]
        public string Text { get; set; }
    }
}