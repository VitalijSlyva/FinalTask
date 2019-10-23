using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Shared;
using System.Collections.Generic;

namespace Rental.WEB.Models.View_Models.Client
{
    /// <summary>
    /// Show payments view model.
    /// </summary>
    public class ShowPaymentsVM
    {
        public List<PaymentDM> Payments { get; set; }

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