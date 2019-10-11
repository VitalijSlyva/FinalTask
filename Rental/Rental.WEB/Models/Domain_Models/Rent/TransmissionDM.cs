using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class TransmissionDM : EntityDM
    {
        public string Category { get; set; }

        public int Count { get; set; }
    }
}