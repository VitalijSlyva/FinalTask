using Rental.WEB.Models.Domain_Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Client
{
    public class ShowUserOrdersVM
    {
        public List<OrderDM> OrdersDM { get; set; }

        public Dictionary<int,string> Statuses { get; set; }
    }
}