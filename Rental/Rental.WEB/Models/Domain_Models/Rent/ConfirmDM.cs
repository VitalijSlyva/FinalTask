using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Confirm domain model.
    /// </summary>
    public class ConfirmDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public OrderDM Order { get; set; }

        [Display(Name = "Принято")]
        public bool IsConfirmed { get; set; }

        [Display(Name ="Коментарий")]
        public string Description { get; set; }
    }
}