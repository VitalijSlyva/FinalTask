using Rental.DAL.Abstracts;
using Rental.DAL.Entities.Rent;
using System;

namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Interface for working with rent entities.
    /// </summary>
    public interface IRentUnitOfWork
    {
        RentRepository<Brand> Brands { get; }

        RentRepository<Car> Cars { get;  }

        RentRepository<Carcass> Carcasses { get;  }

        RentRepository<Confirm> Confirms { get;  }

        RentRepository<Crash> Crashes { get;  }

        RentRepository<Image> Images { get;  }

        RentRepository<Order> Orders { get;  }

        RentRepository<Payment> Payments { get;  }

        RentRepository<Property> Properties { get;  }

        RentRepository<Quality> Qualities { get;  }

        RentRepository<Return> Returns { get;  }

        RentRepository<Transmission> Transmissions { get;  }

        /// <summary>
        /// Save changes.
        /// </summary>
        void Save();
    }
}
