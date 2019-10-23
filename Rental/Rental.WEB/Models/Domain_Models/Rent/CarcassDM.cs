using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Carcass domain model.
    /// </summary>
    public class CarcassDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Кузов")]
        public string Type { get; set; }
    }
}