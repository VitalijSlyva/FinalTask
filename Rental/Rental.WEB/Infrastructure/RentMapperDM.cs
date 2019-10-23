using AutoMapper;
using Rental.BLL.DTO.Rent;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using System.Collections.Generic;

namespace Rental.WEB.Infrastructure
{
    /// <summary>
    /// Mapper for data transfer objects and domain models.
    /// </summary>
    public class RentMapperDM:IRentMapperDM
    {
        public virtual IMapper ToBrandDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BrandDM, BrandDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToBrandDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BrandDTO, BrandDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarcassDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CarcassDM, CarcassDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarcassDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CarcassDTO, CarcassDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CarDM, CarDTO>()
                            .ForMember(x => x.Brand, c => c.MapFrom(k => ToBrandDTO.Map<BrandDM, BrandDTO>(k.Brand)))
                            .ForMember(x => x.Transmission, c => c.MapFrom(k => ToTransmissionDTO.Map<TransmissionDM, TransmissionDTO>(k.Transmission)))
                            .ForMember(x => x.Carcass, c => c.MapFrom(k => ToCarcassDTO.Map<CarcassDM, CarcassDTO>(k.Carcass)))
                            .ForMember(x => x.Quality, c => c.MapFrom(k => ToQualityDTO.Map<QualityDM, QualityDTO>(k.Quality)))
                            .ForMember(x => x.Properties, c => c.MapFrom(k => ToPropertyDTO.Map<IEnumerable<PropertyDM>, List<PropertyDTO>>(k.Properties)))
                            .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDTO.Map<IEnumerable<ImageDM>, List<ImageDTO>>(k.Images))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarDM
        {
            get
            {
                {
                    return new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, CarDM>()
                                .ForMember(x => x.Brand, c => c.MapFrom(k => ToBrandDM.Map<BrandDTO, BrandDM>(k.Brand)))
                                .ForMember(x => x.Transmission, c => c.MapFrom(k => ToTransmissionDM.Map<TransmissionDTO, TransmissionDM>(k.Transmission)))
                                .ForMember(x => x.Carcass, c => c.MapFrom(k => ToCarcassDM.Map<CarcassDTO, CarcassDM>(k.Carcass)))
                                .ForMember(x => x.Quality, c => c.MapFrom(k => ToQualityDM.Map<QualityDTO, QualityDM>(k.Quality)))
                                .ForMember(x => x.Properties, c => c.MapFrom(k => ToPropertyDM.Map<IEnumerable<PropertyDTO>, List<PropertyDM>>(k.Properties)))
                                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDM.Map<IEnumerable<ImageDTO>, List<ImageDM>>(k.Images))))
                        .CreateMapper();
                }
            }
        }

        public virtual IMapper ToConfirmDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ConfirmDM, ConfirmDTO>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDTO.Map<OrderDM, OrderDTO>(k.Order))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToConfirmDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ConfirmDTO, ConfirmDM>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDM.Map<OrderDTO, OrderDM>(k.Order))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCrashDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CrashDM, CrashDTO>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<PaymentDM, PaymentDTO>(k.Payment))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCrashDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CrashDTO, CrashDM>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<PaymentDTO, PaymentDM>(k.Payment))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDM, ImageDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDTO, ImageDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDM, OrderDTO>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<PaymentDM, PaymentDTO>(k.Payment)))
                            .ForMember(x => x.Car, c => c.MapFrom(k => ToCarDTO.Map<CarDM, CarDTO>(k.Car))))
                            .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderDM>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDM.Map<PaymentDTO, PaymentDM>(k.Payment)))
                            .ForMember(x => x.Car, c => c.MapFrom(k => ToCarDM.Map<CarDTO, CarDM>(k.Car)))
                            .ForMember(x=>x.Profile,c=>c.MapFrom(k=>new ProfileDM())))
                            .CreateMapper();
            }
        }

        public virtual IMapper ToPaymentDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PaymentDM, PaymentDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToPaymentDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PaymentDTO, PaymentDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToPropertyDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PropertyDM, PropertyDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToPropertyDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PropertyDTO, PropertyDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToQualityDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<QualityDM, QualityDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToQualityDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<QualityDTO, QualityDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToReturnDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ReturnDM, ReturnDTO>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDTO.Map<OrderDM, OrderDTO>(k.Order)))
                            .ForMember(x => x.Crash, c => c.MapFrom(k => ToCrashDTO.Map<CrashDM, CrashDTO>(k.Crash))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToReturnDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ReturnDTO, ReturnDM>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDM.Map<OrderDTO, OrderDM>(k.Order)))
                            .ForMember(x => x.Crash, c => c.MapFrom(k => ToCrashDM.Map<CrashDTO, CrashDM>(k.Crash))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToTransmissionDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<TransmissionDM, TransmissionDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToTransmissionDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<TransmissionDTO, TransmissionDM>())
                    .CreateMapper();
            }
        }
    }
}