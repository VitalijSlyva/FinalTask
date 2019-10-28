using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.DAL.Interfaces;
using System;

namespace Rental.BLL.Abstracts
{
    /// <summary>
    /// Abstract service for work with units.
    /// </summary>
    public abstract class Service
    {
        protected IRentUnitOfWork RentUnitOfWork;

        protected IIdentityUnitOfWork IdentityUnitOfWork;

        protected IRentMapperDTO RentMapperDTO;

        protected IIdentityMapperDTO IdentityMapperDTO;

        protected ILogService LogService;

        /// <summary>
        /// Create units and mappers for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper for converting database entities to DTO entities</param>
        /// <param name="rentUnit">Rent unit of work</param>
        /// <param name="identityUnit">Udentity unit of work</param>
        /// <param name="identityMapper">Mapper for converting identity entities to BLL classes</param>
        /// <param name="logService">Service for logging</param>
        public Service(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                 IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService logService)
        {
            RentMapperDTO = mapperDTO;
            RentUnitOfWork = rentUnit;
            IdentityMapperDTO = identityMapper;
            IdentityUnitOfWork = identityUnit;
            LogService = logService;
        }

        /// <summary>
        /// Save exception log to database.
        /// </summary>
        /// <param name="e">Exception</param>
        /// <param name="className">Exception class</param>
        /// <param name="actionName">Exception action</param>
        protected void CreateLog(Exception e,string className,string actionName)
        {
            ExceptionLogDTO log = new ExceptionLogDTO()
            {
                ActionName = actionName,
                ClassName = className,
                ExeptionMessage = e.Message,
                StackTrace = e.StackTrace,
                Time = DateTime.Now
            };

            LogService.CreateExeptionLog(log);
        }
    }
}
