using Rental.BLL.DTO.Log;
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

        protected ILogService LogService;

        public Service(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                             IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService logService)
        {
            RentMapperDTO = mapperDTO;
            RentUnitOfWork = rentUnit;
            IdentityMapperDTO = identityMapper;
            IdentityUnitOfWork = identityUnit;
            LogService = logService;
        }

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

        public void Dispose()
        {
            RentUnitOfWork.Dispose();
            IdentityUnitOfWork.Dispose();
            LogService.Dispose();
        }
    }
}
