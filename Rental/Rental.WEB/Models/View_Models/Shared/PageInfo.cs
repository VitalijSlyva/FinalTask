using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.View_Models.Shared
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItem { get; set; }

        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItem / PageSize); } }
    }
}