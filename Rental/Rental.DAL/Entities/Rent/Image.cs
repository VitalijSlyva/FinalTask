using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Image:Entity
    {
        public string Text { get; set; }

        public byte[] Photo { get; set; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
