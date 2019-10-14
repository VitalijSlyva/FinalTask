using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Log
{
    public class ActionLog
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Action { get; set; }

        public DateTime Time { get; set; }
    }
}
