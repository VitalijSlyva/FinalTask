using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Models.Domain_Models.Log
{
    public class ActionLogDM
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Action { get; set; }

        public DateTime Time { get; set; }
    }
}