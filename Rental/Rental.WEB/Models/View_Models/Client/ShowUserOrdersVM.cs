using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System.Collections.Generic;

namespace Rental.WEB.Models.View_Models.Client
{
    /// <summary>
    /// Show user orders view model.
    /// </summary>
    public class ShowUserOrdersVM
    {
        public List<OrderDM> OrdersDM { get; set; }

        public Dictionary<int,string> Statuses { get; set; }

        public List<Filter> Filters { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<string> SortModes { get; set; }

        public int SelectedMode;
    }
}