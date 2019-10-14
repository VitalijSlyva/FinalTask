using Rental.DAL.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Initializers
{
    internal class LogInitializer:DropCreateDatabaseIfModelChanges<LogContext>
    {
        protected override void Seed(LogContext context)
        {
            base.Seed(context);
        }
    }
}
