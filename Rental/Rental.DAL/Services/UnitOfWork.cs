using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Services
{
    public class UnitOfWork:IUnitOfWork
    {
        private IIdentityUnitOfWork _identity;

        private IRentUnitOfWork _rent;

        private readonly string _connectionRent;

        private readonly string _connectionIdentity;

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

        public void Dispose()
        {
            Identity.Dispose();
            Rent.Dispose();
        }
    }
}
