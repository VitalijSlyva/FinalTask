using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Quality:Entity
    {
        public string Text { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
