using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class CrashDTO : EntityDTO
    {
        public string Description { get; set; }

        public PaymentDTO Payment { get; set; }
    }
}
