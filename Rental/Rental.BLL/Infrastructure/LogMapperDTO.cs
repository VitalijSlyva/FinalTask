using AutoMapper;
using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Log;

namespace Rental.BLL.Infrastructure
{
    /// <summary>
    /// Mapper for log entity and data transfer objects.
    /// </summary>
    internal class LogMapperDTO : ILogMapperDTO
    {
        public IMapper ToExceptionLogDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ExceptionLog, ExceptionLogDTO>()).CreateMapper();
            }
        }

        public IMapper ToExceptionLog
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ExceptionLogDTO, ExceptionLog>()).CreateMapper();
            }
        }

        public IMapper ToActionLogDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ActionLog, ActionLogDTO>()).CreateMapper();
            }
        }

        public IMapper ToActionLog
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ActionLogDTO, ActionLog>()).CreateMapper();
            }
        }
    }
}
