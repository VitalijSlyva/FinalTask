using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Rent
{
    public class IndexVM
    {
        public List<CarDM> Cars { get; set; }

        public List<Filter> Filters { get; set; }

        public int? PriceMin { get; set; }

        public int? PriceMax { get; set; }

        public int? CurrentPriceMin { get; set; }

        public int? CurrentPriceMax { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<string> SortModes { get; set; }

        public int SelectedMode;
    }
    
}