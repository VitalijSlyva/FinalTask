using AutoMapper;
using Rental.BLL.DTO.Log;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Infrastructure
{
    public class LogMapperDM : ILogMapperDM
    {
        public IMapper ToExceptionLogDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ExceptionLogDM, ExceptionLogDTO>()).CreateMapper();
            }
        }

        public IMapper ToExceptionLogDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ExceptionLogDTO, ExceptionLogDM>()).CreateMapper();
            }
        }

        public IMapper ToActionLogDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ActionLogDM, ActionLogDTO>()).CreateMapper();
            }
        }

        public IMapper ToActionLogDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ActionLogDTO, ActionLogDM>()).CreateMapper();
            }
        }
    }
}