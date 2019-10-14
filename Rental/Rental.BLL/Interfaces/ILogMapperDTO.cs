using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface ILogMapperDTO
    {
        IMapper ToExceptionLogDTO { get; }

        IMapper ToExceptionLog { get; }

        IMapper ToActionLogDTO { get; }

        IMapper ToActionLog { get; }
    }
}
