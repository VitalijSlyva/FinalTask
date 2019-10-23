using Ninject.Modules;
using Rental.BLL.Interfaces;
using Rental.BLL.Services;
using Rental.WEB.Interfaces;

namespace Rental.WEB.Infrastructure
{
    /// <summary>
    /// Module for dependency injection.
    /// </summary>
    public class ServiceModuleWEB : NinjectModule
    {
        /// <summary>
        /// Create binds.
        /// </summary>
        public override void Load()
        {
            Bind<IAccountService>().To<AccountService>();
            Bind<IClientService>().To<ClientService>();
            Bind<IRentService>().To<RentService>();
            Bind<IManagerService>().To<ManagerService>();
            Bind<ILogService>().To<LogService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IIdentityMapperDM>().To<IdentityMapperDM>();
            Bind<IRentMapperDM>().To<RentMapperDM>();
            Bind<ILogMapperDM>().To<LogMapperDM>();
            Bind<ILogWriter>().To<LogWriter>();
        }
    }
}