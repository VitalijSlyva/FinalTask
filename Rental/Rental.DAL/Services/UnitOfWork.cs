using Rental.DAL.Interfaces;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Common unit of work.
    /// </summary>
    public class UnitOfWork:IUnitOfWork
    {
        private IIdentityUnitOfWork _identity;

        private IRentUnitOfWork _rent;

        private readonly string _connectionRent;

        private readonly string _connectionIdentity;

        /// <summary>
        /// Create connection with databases.
        /// </summary>
        /// <param name="connectionRent">Connection string for rent context</param>
        /// <param name="connectionIdentity">Connection string for identity context</param>
        public UnitOfWork(string connectionRent,string connectionIdentity)
        {
            _connectionRent = connectionRent;
            _connectionIdentity = connectionIdentity;
        }

        public IRentUnitOfWork Rent
        {
            get
            {
                if (_rent == null)
                    _rent = new RentUnitOfWork(_connectionRent);
                return _rent;
            }
        }

        public IIdentityUnitOfWork Identity
        {
            get
            {
                if (_identity == null)
                    _identity = new IdentityUnitOfWork(_connectionIdentity);
                return _identity;
            }
        }

        /// <summary>
        /// Dispose contexts.
        /// </summary>
        public void Dispose()
        {
            Identity.Dispose();
            Rent.Dispose();
        }
    }
}
