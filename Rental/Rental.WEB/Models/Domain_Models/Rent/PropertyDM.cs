using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class PropertyDM:EntityDM
    {
        public string Name { get; set; }

        public string Text { get; set; }
    }
}