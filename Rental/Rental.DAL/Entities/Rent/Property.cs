using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Property:Entity
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
