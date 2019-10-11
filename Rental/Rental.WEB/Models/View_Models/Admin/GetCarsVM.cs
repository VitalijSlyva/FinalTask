using Rental.WEB.Models.Domain_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Admin
{
    public class GetCarsVM
    {
        public List<CarDM> CarsDM { get; set; }
    }
}