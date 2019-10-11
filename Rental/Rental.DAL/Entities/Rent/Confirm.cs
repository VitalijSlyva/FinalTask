using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Confirm:Entity
    {
        public string ManagerId { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public bool IsConfirmed { get; set; }

        public string Description { get; set; }
    }
}
