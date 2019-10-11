using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IIdentityMapperDTO
    {
        IMapper ToUserDTO { get; }

        IMapper ToProfileDTO { get; }

        IMapper ToProfile { get; }
    }
}
