using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class TransmissionDTO : EntityDTO
    {
        public string Category { get; set; }

        public int Count { get; set; }
    }
}
