using Ninject.Modules;
using Rental.BLL.Interfaces;
using Rental.DAL.Interfaces;
using Rental.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Infrastructure
{
    public class ServiceModuleBLL : NinjectModule
    {
        private string _rentConnectionString;

        private string _identityConnectionString;

        public ServiceModuleBLL(string identityConnectionString,string rentConnectionString)
        {
            _identityConnectionString = identityConnectionString;
            _rentConnectionString = rentConnectionString;
        }

        public override void Load()
        {
            Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_identityConnectionString);
            Bind<IRentUnitOfWork>().To<RentUnitOfWork>().WithConstructorArgument(_rentConnectionString);
            Bind<IRentMapperDTO>().To<RentMapperDTO>();
            Bind<IIdentityMapperDTO>().To<IdentityMapperDTO>();
        }
    }
}
