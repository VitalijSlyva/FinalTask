﻿using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Manager
{
    public class ShowReturnsVM
    {
        public List<OrderDM> Orders { get; set; }

        public List<Filter> Filters { get; set; }
    }
}