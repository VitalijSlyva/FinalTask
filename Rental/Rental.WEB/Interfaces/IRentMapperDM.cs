using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Interfaces
{
    public interface IRentMapperDM
    {
        IMapper ToBrandDTO { get; }

        IMapper ToBrandDM { get; }

        IMapper ToCarcassDTO { get; }

        IMapper ToCarcassDM { get; }

        IMapper ToCarDTO { get; }

        IMapper ToCarDM { get; }

        IMapper ToConfirmDTO { get; }

        IMapper ToConfirmDM { get; }

        IMapper ToCrashDTO { get; }

        IMapper ToCrashDM { get; }

        IMapper ToImageDTO { get; }

        IMapper ToImageDM { get; }

        IMapper ToOrderDTO { get; }

        IMapper ToOrderDM { get; }

        IMapper ToPaymentDTO { get; }

        IMapper ToPaymentDM { get; }

        IMapper ToPropertyDTO { get; }

        IMapper ToPropertyDM { get; }

        IMapper ToQualityDTO { get; }

        IMapper ToQualityDM { get; }

        IMapper ToReturnDTO { get; }

        IMapper ToReturnDM { get; }

        IMapper ToTransmissionDTO { get; }

        IMapper ToTransmissionDM { get; }
    }
}