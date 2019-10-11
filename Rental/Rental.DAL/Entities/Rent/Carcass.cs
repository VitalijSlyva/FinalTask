using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Carcass :Entity
    {
        public string Type { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
