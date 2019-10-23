using Rental.BLL.DTO.Identity;

namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Confirm data transfer object.
    /// </summary>
    public class ConfirmDTO : EntityDTO
    {
        public User User{ get; set; }

        public OrderDTO Order { get; set; }

        public bool IsConfirmed { get; set; }

        public string Description { get; set; }
    }
}
