using Ninject.Modules;
using Rental.BLL.Interfaces;
using Rental.BLL.Services;
using Rental.WEB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Infrastructure
{
    public class ServiceModuleWEB : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountService>().To<AccountService>();
            Bind<IClientService>().To<ClientService>();
            Bind<IRentService>().To<RentService>();
            Bind<IManagerService>().To<ManagerService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IIdentityMapperDM>().To<IdentityMapperDM>();
            Bind<IRentMapperDM>().To<RentMapperDM>();
        }
    }
}