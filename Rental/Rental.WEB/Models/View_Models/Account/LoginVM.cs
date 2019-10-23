using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.View_Models.Account
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginVM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не является почтой")]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}