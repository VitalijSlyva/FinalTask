using Rental.DAL.Abstracts;
using Rental.DAL.Entities.Rent;
using Rental.DAL.Interfaces;
using System;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Unit for work with rent context.
    /// </summary>
    public class RentUnitOfWork : IRentUnitOfWork
    {
        private EF.Contexts.RentContext _rentContext;

        private RentRepository<Brand> _brands;

        private RentRepository<Car> _cars;

        private RentRepository<Carcass> _carcasses;

        private RentRepository<Confirm> _confirms;

        private RentRepository<Crash> _crashes;

        private RentRepository<Image> _images;

        private RentRepository<Order> _orders;

        private RentRepository<Payment> _payments;

        private RentRepository<Property> _properties;

        private RentRepository<Quality> _qualities;

        private RentRepository<Return> _returns;

        private RentRepository<Transmission> _transmissions;

        /// <summary>
        /// Create context with connection.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public RentUnitOfWork(string connection)
        {
            _rentContext = new EF.Contexts.RentContext(connection);
        }

        public RentRepository<Brand> Brands
        {
            get
            {
                if (_brands == null)
                    _brands = new Repository<Brand>(_rentContext);
                return _brands;
            }
        }

        public RentRepository<Car> Cars
        {
            get
            {
                if (_cars == null)
                    _cars = new Repository<Car>(_rentContext);
                return _cars;
            }
        }

        public RentRepository<Carcass> Carcasses
        {
            get
            {
                if (_carcasses == null)
                    _carcasses = new Repository<Carcass>(_rentContext);
                return _carcasses;
            }
        }

        public RentRepository<Confirm> Confirms
        {
            get
            {
                if (_confirms == null)
                    _confirms = new Repository<Confirm>(_rentContext);
                return _confirms;
            }
        }

        public RentRepository<Crash> Crashes
        {
            get
            {
                if (_crashes == null)
                    _crashes = new Repository<Crash>(_rentContext);
                return _crashes;
            }
        }

        public RentRepository<Image> Images
        {
            get
            {
                if (_images == null)
                   _images = new Repository<Image>(_rentContext);
                return _images;
            }
        }

        public RentRepository<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new Repository<Order>(_rentContext);
                return _orders;
            }
        }

        public RentRepository<Payment> Payments
        {
            get
            {
                if (_payments == null)
                    _payments = new Repository<Payment>(_rentContext);
                return _payments;
            }
        }

        public RentRepository<Property> Properties
        {
            get
            {
                if (_properties == null)
                    _properties = new Repository<Property>(_rentContext);
                return _properties;
            }
        }

        public RentRepository<Quality> Qualities
        {
            get
            {
                if (_qualities == null)
                    _qualities = new Repository<Quality>(_rentContext);
                return _qualities;
            }
        }

        public RentRepository<Return> Returns
        {
            get
            {
                if (_returns == null)
                    _returns = new Repository<Return>(_rentContext);
                return _returns;
            }
        }

        public RentRepository<Transmission> Transmissions
        {
            get
            {
                if (_transmissions == null)
                    _transmissions = new Repository<Transmission>(_rentContext);
                return _transmissions;
            }
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void Save()
        {
            _rentContext.SaveChanges();
        }

        private bool _Disposed = false;

        /// <summary>
        /// Call method for disposing.
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                    _rentContext.Dispose();
            }
            _Disposed = true;
        }

        /// <summary>
        /// Dispose context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
