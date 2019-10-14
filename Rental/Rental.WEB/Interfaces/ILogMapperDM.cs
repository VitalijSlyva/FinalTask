using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.WEB.Interfaces
{
    public interface ILogMapperDM
    {
        IMapper ToExceptionLogDTO { get; }

        IMapper ToExceptionLogDM { get; }

        IMapper ToActionLogDTO { get; }

        IMapper ToActionLogDM { get; }
    }
}
