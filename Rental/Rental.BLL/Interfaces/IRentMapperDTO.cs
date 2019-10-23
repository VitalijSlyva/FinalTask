using AutoMapper;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface mapper for rent entities and data transfer objects.
    /// </summary>
    public interface IRentMapperDTO
    {
        IMapper ToBrandDTO { get; }

        IMapper ToBrand { get; }

        IMapper ToCarcassDTO { get; }

        IMapper ToCarcass { get; }

        IMapper ToCarDTO { get; }

        IMapper ToCar { get; }

        IMapper ToConfirmDTO { get; }

        IMapper ToConfirm { get; }

        IMapper ToCrashDTO { get; }

        IMapper ToCrash { get; }

        IMapper ToImageDTO { get; }

        IMapper ToImage { get; }

        IMapper ToOrderDTO { get; }

        IMapper ToOrder { get; }

        IMapper ToPaymentDTO { get; }

        IMapper ToPayment { get; }

        IMapper ToPropertyDTO { get; }

        IMapper ToProperty { get; }

        IMapper ToQualityDTO { get; }

        IMapper ToQuality { get; }

        IMapper ToReturnDTO { get; }

        IMapper ToReturn { get; }

        IMapper ToTransmissionDTO { get; }

        IMapper ToTransmission { get; }
    }
}
