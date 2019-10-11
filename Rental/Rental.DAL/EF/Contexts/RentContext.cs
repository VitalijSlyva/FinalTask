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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasOptional(f => f.Confirm)
            .WithRequired(s => s.Order);
            modelBuilder.Entity<Crash>()
            .HasOptional(f => f.Payment)
            .WithRequired(s => s.Crash);
            modelBuilder.Entity<Crash>()
            .HasOptional(f => f.Return)
            .WithRequired(s => s.Crash);
            modelBuilder.Entity<Return>()
            .HasOptional(f => f.Order)
            .WithRequired(s => s.Return);
            modelBuilder.Entity<Payment>()
            .HasOptional(f => f.Order)
            .WithRequired(s => s.Payment);
            base.OnModelCreating(modelBuilder);
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
