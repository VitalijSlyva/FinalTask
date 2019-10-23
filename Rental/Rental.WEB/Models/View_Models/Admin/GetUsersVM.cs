using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.View_Models.Shared;
using System.Collections.Generic;

namespace Rental.WEB.Models.View_Models.Admin
{
    /// <summary>
    /// Get users view model.
    /// </summary>
    public class GetUsersVM
    {
        public List<UserDM> UsersDM { get; set; }

        public Dictionary<string,string> Roles { get; set; }

        public Dictionary<string, bool> Banns { get; set; }

        public List<Filter> Filters { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<string> SortModes { get; set; }

        public int SelectedMode;
    }
}