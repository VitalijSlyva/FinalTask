using AutoMapper;
using Rental.BLL.DTO.Identity;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;

namespace Rental.WEB.Infrastructure
{
    /// <summary>
    /// Mapper for identity data transfer objects and identity models.
    /// </summary>
    public class IdentityMapperDM:IIdentityMapperDM
    {
        public IMapper ToUserDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<User, UserDM>())
                .CreateMapper();
            }
        }

        public IMapper ToProfileDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ProfileDM, ProfileDTO>())
                .CreateMapper();
            }
        }

        public IMapper ToProfileDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ProfileDTO, ProfileDM>()
                   .ForMember(x => x.User, k => k.MapFrom(c => ToUserDM.Map<User, UserDM>(c.User))))
                .CreateMapper();
            }
        }
    }
}