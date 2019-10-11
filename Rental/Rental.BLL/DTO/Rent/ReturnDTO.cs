using Rental.BLL.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class ReturnDTO : EntityDTO
    {
        public User User { get; set; }

        public CrashDTO Crash { get; set; }

        public bool IsReturned { get; set; }

        public OrderDTO Order { get; set; }
    }
}
