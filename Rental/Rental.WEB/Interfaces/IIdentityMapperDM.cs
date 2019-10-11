using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Interfaces
{
    public interface IIdentityMapperDM
    {
        IMapper ToUserDM { get; }

        IMapper ToProfileDTO { get; }

        IMapper ToProfileDM { get; }
    }
}