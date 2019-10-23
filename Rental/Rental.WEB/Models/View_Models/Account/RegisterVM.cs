using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.View_Models.Account
{
    /// <summary>
    /// Register view model.
    /// </summary>
    public class RegisterVM
    {
        [Required(ErrorMessage ="Поле должно быть заполнено")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Не является почтой")]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Слишком короткий")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Compare("Password",ErrorMessage ="Пароли  не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name="Повторите пароль")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}