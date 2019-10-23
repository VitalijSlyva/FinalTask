using Ninject.Modules;
using Rental.BLL.Interfaces;
using Rental.DAL.Interfaces;
using Rental.DAL.Services;

namespace Rental.BLL.Infrastructure
{
    /// <summary>
    /// Module for dependency injection.
    /// </summary>
    public class ServiceModuleBLL : NinjectModule
    {
        private string _rentConnectionString;

        private string _identityConnectionString;

        private string _logConnectionString;

        /// <summary>
        /// Create connections with databases.
        /// </summary>
        /// <param name="identityConnectionString">Identity database connection string</param>
        /// <param name="rentConnectionString">Rent database connection string.</param>
        /// <param name="logConnectionString">Log database connection string.</param>
        public ServiceModuleBLL(string identityConnectionString,string rentConnectionString, string logConnectionString)
        {
            _identityConnectionString = identityConnectionString;
            _rentConnectionString = rentConnectionString;
            _logConnectionString = logConnectionString;
        }

        /// <summary>
        /// Create binds.
        /// </summary>
        public override void Load()
        {
            Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_identityConnectionString);
            Bind<IRentUnitOfWork>().To<RentUnitOfWork>().WithConstructorArgument(_rentConnectionString);
            Bind<ILogUnitOfWork>().To<LogUnitOfWork>().WithConstructorArgument(_logConnectionString);
            Bind<IRentMapperDTO>().To<RentMapperDTO>();
            Bind<IIdentityMapperDTO>().To<IdentityMapperDTO>();
            Bind<ILogMapperDTO>().To<LogMapperDTO>();
        }
    }
}
