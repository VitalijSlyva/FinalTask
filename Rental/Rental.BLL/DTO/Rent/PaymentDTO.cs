using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class PaymentDTO : EntityDTO
    {
        public string TransactionId { get; set; }

        public bool IsPaid { get; set; }

        public int Price { get; set; }
    }
}
