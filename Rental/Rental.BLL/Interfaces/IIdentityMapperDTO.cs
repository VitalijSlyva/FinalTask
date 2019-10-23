using AutoMapper;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for identity mapper.
    /// </summary>
    public interface IIdentityMapperDTO
    {
        IMapper ToUserDTO { get; }

        IMapper ToProfileDTO { get; }

        IMapper ToProfile { get; }
    }
}
