using Rental.WEB.Models.Domain_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Admin
{
    public class CreateVM
    {
        public CarDM Car { get; set; }

        public string[] PropertyNames { get; set; }

        public string[] PropertyValues { get; set; }

        public HttpPostedFileBase[] Images { get; set; }

        public string[] Alts { get; set; }

        public string[] Photos { get; set; }
    }
}