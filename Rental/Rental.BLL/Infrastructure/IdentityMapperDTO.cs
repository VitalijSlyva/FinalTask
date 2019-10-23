using AutoMapper;
using Rental.BLL.DTO.Identity;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;

namespace Rental.BLL.Infrastructure
{
    /// <summary>
    /// Mapper for identity objects and data transfer objects.
    /// </summary>
    internal class IdentityMapperDTO : IIdentityMapperDTO
    {
        public IMapper ToUserDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, User>()
                .ForMember(x => x.Password, k => k.MapFrom(c => c.PasswordHash)))
                .CreateMapper();
            }
        }

        public IMapper ToProfileDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Identity.Profile, ProfileDTO>()
                .ForMember(x => x.User, k => k.MapFrom(c => ToUserDTO.Map<ApplicationUser, User>(c.ApplicationUser))))
                .CreateMapper();
            }
        }

        public IMapper ToProfile
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ProfileDTO, DAL.Entities.Identity.Profile>())
                .CreateMapper();
            }
        }
    }
}
