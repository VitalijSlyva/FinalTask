using Rental.DAL.Abstracts;
using Rental.DAL.Entities.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IRentUnitOfWork : IDisposable
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

        void Save();
    }
}
