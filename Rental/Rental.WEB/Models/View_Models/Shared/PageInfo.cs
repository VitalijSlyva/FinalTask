using System;

namespace Rental.WEB.Models.View_Models.Shared
{
    /// <summary>
    /// Paging information.
    /// </summary>
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItem { get; set; }

        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItem / PageSize); } }
    }
}