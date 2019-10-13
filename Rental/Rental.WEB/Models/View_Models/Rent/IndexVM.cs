using Rental.WEB.Models.Domain_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Rent
{
    public class IndexVM
    {
        public List<CarDM> Cars { get; set; }
    }
}