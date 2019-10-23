using Rental.BLL.DTO.Identity;
using System;

namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Order data transfer object.
    /// </summary>
    public class OrderDTO : EntityDTO
    {
        public ProfileDTO Profile { set; get; }

        public CarDTO Car { get; set; }

        public bool WithDriver { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public PaymentDTO Payment { get; set; }
    }
}
