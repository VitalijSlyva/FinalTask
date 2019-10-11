﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Transmission :Entity
    {
        public string Category { get; set; }

        public int Count { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
