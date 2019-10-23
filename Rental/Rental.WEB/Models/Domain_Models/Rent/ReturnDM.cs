using System.ComponentModel.DataAnnotations;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    /// <summary>
    /// Return domain model.
    /// </summary>
    public class ReturnDM : EntityDM
    {
        public Identity.UserDM User { get; set; }

        public CrashDM Crash { get; set; }

        [Display(Name = "Возвращено")]
        public bool IsReturned { get; set; }

        public OrderDM Order { get; set; }
    }
}