using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Common fileds for domain models.
    /// </summary>
    public class EntityDM
    {
        [Display(Name = "Идентификационный номер")]
        public int Id { get; set; }
    }
}