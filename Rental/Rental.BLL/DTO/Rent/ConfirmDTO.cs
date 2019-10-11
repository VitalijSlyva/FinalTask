using Rental.BLL.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Rent
{
    public class ConfirmDTO : EntityDTO
    {
        public User User{ get; set; }

        public OrderDTO Order { get; set; }

        public bool IsConfirmed { get; set; }

        public string Description { get; set; }
    }
}
