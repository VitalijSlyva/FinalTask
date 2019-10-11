using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Rent
{
    public class ImageDM : EntityDM
    {
        public string Text { get; set; }

        public byte[] Photo { get; set; }
    }
}