using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Rent;
using System.Data.Entity;

namespace Rental.DAL.EF.Contexts
{
    /// <summary>
    /// Context for rent entities.
    /// </summary>
    public class RentContext:DbContext
    {
        /// <summary>
        /// Creates initializator for database.
        /// </summary>
        static RentContext()
        {
            Database.SetInitializer<RentContext>(new RentInitializer());
        }

        /// <summary>
        /// Default builder.
        /// </summary>
        public RentContext() { }

        /// <summary>
        /// Connect to database.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public RentContext(string connection) : base(connection)
        {

        }

        /// <summary>
        /// Brand table.
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Car table.
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Carcass table.
        /// </summary>
        public DbSet<Carcass> Carcasses { get; set; }

        /// <summary>
        /// Confirm table.
        /// </summary>
        public DbSet<Confirm> Confirms { get; set; }
        
        /// <summary>
        /// Crash table.
        /// </summary>
        public DbSet<Crash> Crashes { get; set; }

        /// <summary>
        /// Image table.
        /// </summary>
        public DbSet<Image> Images { get; set; }

        /// <summary>
        /// Order table.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Payment table.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Property table.
        /// </summary>
        public DbSet<Property> Properties { get; set; }

        /// <summary>
        /// Quality table.
        /// </summary>
        public DbSet<Quality> Qualities { get; set; }

        /// <summary>
        /// Return table.
        /// </summary>
        public DbSet<Return> Returns { get; set; }

        /// <summary>
        /// Transmission table.
        /// </summary>
        public DbSet<Transmission> Transmissions { get; set; }
    }
}
