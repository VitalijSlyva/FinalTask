using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Entities.Rent
{
    public class Return:Entity
    { 
        public string ManagerId { get; set; }

        //   public int? CrashId { get; set; }

        // public virtual Crash Crash { get; set; }

        public virtual ICollection<Crash> Crash { get; set; }

        public bool IsReturned { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
