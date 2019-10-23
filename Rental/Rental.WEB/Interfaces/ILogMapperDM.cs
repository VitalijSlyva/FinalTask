using AutoMapper;

namespace Rental.WEB.Interfaces
{
    /// <summary>
    /// Interface mapper for log models.
    /// </summary>
    public interface ILogMapperDM
    {
        IMapper ToExceptionLogDTO { get; }

        IMapper ToExceptionLogDM { get; }

        IMapper ToActionLogDTO { get; }

        IMapper ToActionLogDM { get; }
    }
}
