using AutoMapper;
using Rental.BLL.DTO.Identity;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Infrastructure
{
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