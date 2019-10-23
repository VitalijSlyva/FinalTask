using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System.Collections.Generic;

namespace Rental.WEB.Models.View_Models.Admin
{
    /// <summary>
    /// Get cars view model.
    /// </summary>
    public class GetCarsVM
    {
        public List<CarDM> CarsDM { get; set; }

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