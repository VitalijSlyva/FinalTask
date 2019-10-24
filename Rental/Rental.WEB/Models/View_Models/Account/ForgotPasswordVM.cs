using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Account
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не является почтой")]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}