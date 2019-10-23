using AutoMapper;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// interface log mapper.
    /// </summary>
    public interface ILogMapperDTO
    {
        IMapper ToExceptionLogDTO { get; }

        IMapper ToExceptionLog { get; }

        IMapper ToActionLogDTO { get; }

        IMapper ToActionLog { get; }
    }
}
