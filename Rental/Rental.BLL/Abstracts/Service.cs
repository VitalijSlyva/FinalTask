using Rental.BLL.Interfaces;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Abstracts
{
    public abstract class Service:IDisposable
    {
        protected IRentUnitOfWork RentUnitOfWork;

        protected IIdentityUnitOfWork IdentityUnitOfWork;

        protected IRentMapperDTO RentMapperDTO;

        protected IIdentityMapperDTO IdentityMapperDTO;

        public Service(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                             IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper)
        {
            RentMapperDTO = mapperDTO;
            RentUnitOfWork = rentUnit;
            IdentityMapperDTO = identityMapper;
            IdentityUnitOfWork = identityUnit;
        }

        public void Dispose()
        {
            RentUnitOfWork.Dispose();
            IdentityUnitOfWork.Dispose();
        }
    }
}
