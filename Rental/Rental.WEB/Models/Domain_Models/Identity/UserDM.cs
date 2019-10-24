using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Identity
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserDM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не является почтой")]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public bool ConfirmedEmail { get; set; }
    }
}