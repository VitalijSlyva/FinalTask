using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Identity
{
    public class ProfileDM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Неверная дата")]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Неверная дата")]
        [Display(Name = "Дата завершеня действия")]
        public DateTime DateOfExpiry { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный номер")]
        [Display(Name = "Номер телефона")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Гражданство")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(20, MinimumLength = 9, ErrorMessage ="Неверна длинна")]
        [Display(Name = "Номер документа")]
        public string Record { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Неверная дата")]
        [Display(Name = "Дата выдачи")]
        public DateTime DateOfIssue { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(20,MinimumLength =10, ErrorMessage = "Неверна длинна")]
        [Display(Name = "RNTRC")]
        public string RNTRC { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Неверна длинна")]
        [Display(Name = "Номер отделения выдачи")]
        public string Authory { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Место рождения")]
        public string PlaceOfBirth { get; set; }

        public UserDM User { get; set; }
    }
}