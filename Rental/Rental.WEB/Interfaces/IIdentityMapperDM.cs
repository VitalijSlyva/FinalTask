using AutoMapper;

namespace Rental.WEB.Interfaces
{
    /// <summary>
    /// Interface mapper for identity models.
    /// </summary>
    public interface IIdentityMapperDM
    {
        IMapper ToUserDM { get; }

        IMapper ToProfileDTO { get; }

        IMapper ToProfileDM { get; }
    }
}