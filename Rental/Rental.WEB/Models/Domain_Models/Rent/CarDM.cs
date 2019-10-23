using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Car domain model.
    /// </summary>
    public class CarDM : EntityDM
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Модeль")]
        public string Model { set; get; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public BrandDM Brand { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Номер")]
        [RegularExpression("[A-Za-z]{2}[0-9]{4}[A-Za-z]{2}",ErrorMessage ="Неверный номер")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Цена за сутки")]
        [Range(1,100000,ErrorMessage ="Неверное число")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Количество дверей")]
        [Range(1, 10, ErrorMessage = "Неверное число")]
        public int Doors { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Вместительность")]
        [Range(1, 100, ErrorMessage = "Неверное число")]
        public int Кoominess { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Топливо")]
        public string Fuel { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Грузоподъемность кг")]
        [Range(1, 10000, ErrorMessage = "Неверное число")]
        public int Carrying { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Объем двигателя л")]
        [Range(1, 100, ErrorMessage = "Неверное число")]
        public double EngineVolume { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Л.С.")]
        [Range(1, 1000, ErrorMessage = "Неверное число")]
        public double Hoursepower { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Date, ErrorMessage = "Не является датой")]
        [Display(Name = "Дата выпуска")]
        public DateTime DateOfCreate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public TransmissionDM Transmission { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public CarcassDM Carcass { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public QualityDM Quality { get; set; }

        public List<PropertyDM> Properties { get; set; }

        public List<ImageDM> Images { get; set; }

        public bool IsDeleted { get; set; }
    }
}