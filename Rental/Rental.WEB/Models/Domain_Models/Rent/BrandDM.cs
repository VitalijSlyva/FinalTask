using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Brand domain model.
    /// </summary>
    public class BrandDM:EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name="Марка")]
        public string Name { get; set; }
    }
}