using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Brand :Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
