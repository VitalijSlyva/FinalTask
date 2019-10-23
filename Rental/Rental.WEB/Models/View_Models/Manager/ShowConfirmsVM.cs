using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System.Collections.Generic;

namespace Rental.WEB.Models.View_Models.Manager
{
    /// <summary>
    /// Show confirms view model.
    /// </summary>
    public class ShowConfirmsVM
    {
        public List<OrderDM> Orders { get; set; }

        public List<Filter> Filters { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<string> SortModes { get; set; }

        public int SelectedMode;
    }
}