using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Rent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Contexts
{
    public class RentContext:DbContext
    {
        static RentContext()
        {
            Database.SetInitializer<RentContext>(new RentInitializer());
        }

        public RentContext() { }

        public RentContext(string connection) : base(connection)
        {

        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Carcass> Carcasses { get; set; }

        public DbSet<Confirm> Confirms { get; set; }

        public DbSet<Crash> Crashes { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Quality> Qualities { get; set; }

        public DbSet<Return> Returns { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }
    }
}
