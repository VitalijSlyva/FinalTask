using Rental.DAL.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Initializers
{
    internal class IdentityInitializer : DropCreateDatabaseIfModelChanges<IdentityContext>
    {
        protected override void Seed(IdentityContext context)
        {
            base.Seed(context);
        }
    }
}
