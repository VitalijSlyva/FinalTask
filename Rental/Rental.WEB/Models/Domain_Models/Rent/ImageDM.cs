using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Image domain model.
    /// </summary>
    public class ImageDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        public byte[] Photo { get; set; }
    }
}