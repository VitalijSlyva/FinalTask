using Rental.BLL.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
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
