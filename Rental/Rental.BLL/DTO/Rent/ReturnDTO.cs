using Rental.BLL.DTO.Identity;

namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Rent data transfer object.
    /// </summary>
    public class ReturnDTO : EntityDTO
    {
        public User User { get; set; }

        public CrashDTO Crash { get; set; }

        public bool IsReturned { get; set; }

        public OrderDTO Order { get; set; }
    }
}
