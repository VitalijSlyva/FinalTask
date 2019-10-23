using AutoMapper;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Rent;
using System.Collections.Generic;
using System.Linq;

namespace Rental.BLL.Infrastructure
{
    /// <summary>
    /// Mapper for rent entities and data transfer objects.
    /// </summary>
    internal class RentMapperDTO : IRentMapperDTO
    {
        public virtual IMapper ToBrandDTO
        {
            get {
                return new MapperConfiguration(cfg => cfg.CreateMap<Brand, BrandDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToBrand
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BrandDTO, Brand>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarcassDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Carcass, CarcassDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarcass
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CarcassDTO, Carcass>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCarDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>()
                            .ForMember(x=>x.Brand,c=>c.MapFrom(k=>ToBrandDTO.Map<Brand,BrandDTO>(k.Brand)))
                            .ForMember(x=>x.Transmission,c=>c.MapFrom(k=>ToTransmissionDTO.Map<Transmission,TransmissionDTO>(k.Transmission)))
                            .ForMember(x=>x.Carcass,c=>c.MapFrom(k=>ToCarcassDTO.Map<Carcass,CarcassDTO>(k.Carcass)))
                            .ForMember(x=>x.Quality,c=>c.MapFrom(k=>ToQualityDTO.Map<Quality,QualityDTO>(k.Quality)))
                            .ForMember(x=>x.Properties,c=>c.MapFrom(k=>ToPropertyDTO.Map<ICollection<Property>,List<PropertyDTO>>(k.Properties)))
                            .ForMember(x=>x.Images,c=>c.MapFrom(k=>ToImageDTO.Map<ICollection<Image>,List<ImageDTO>>(k.Images))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCar
        {
            get
            {
                {
                    return new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>()
                                .ForMember(x => x.Brand, c => c.MapFrom(k => ToBrand.Map<BrandDTO, Brand>(k.Brand)))
                                .ForMember(x => x.Transmission, c => c.MapFrom(k => ToTransmission.Map<TransmissionDTO, Transmission>(k.Transmission)))
                                .ForMember(x => x.Carcass, c => c.MapFrom(k => ToCarcass.Map<CarcassDTO, Carcass>(k.Carcass)))
                                .ForMember(x => x.Quality, c => c.MapFrom(k => ToQuality.Map<QualityDTO, Quality>(k.Quality)))
                                .ForMember(x => x.Properties, c => c.MapFrom(k => ToProperty.Map<IEnumerable<PropertyDTO>, List<Property>>(k.Properties)))
                                .ForMember(x => x.Images, c => c.MapFrom(k => ToImage.Map<IEnumerable<ImageDTO>, List<Image>>(k.Images))))
                        .CreateMapper();
                }
            }
        }

        public virtual IMapper ToConfirmDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Confirm, ConfirmDTO>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDTO.Map<Order, OrderDTO>(k.Order))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToConfirm
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ConfirmDTO, Confirm>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrder.Map<OrderDTO, Order>(k.Order))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCrashDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Crash, CrashDTO>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<Payment, PaymentDTO>(k.Payment.FirstOrDefault()))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToCrash
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<CrashDTO, Crash>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<IEnumerable<PaymentDTO>,ICollection<Payment>>( new[] { k.Payment }))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Image, ImageDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImage
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDTO, Image>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Order,OrderDTO>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPaymentDTO.Map<Payment, PaymentDTO>(k.Payment.FirstOrDefault())))
                            .ForMember(x => x.Car, c => c.MapFrom(k => ToCarDTO.Map<Car, CarDTO>(k.Car))))
                            .CreateMapper();
            }
        }

        public virtual IMapper ToOrder
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>()
                            .ForMember(x => x.Payment, c => c.MapFrom(k => ToPayment.Map<IEnumerable<PaymentDTO>,ICollection<Payment>>(new[] { k.Payment })))
                            .ForMember(x => x.Car, c => c.MapFrom(k => ToCar.Map<CarDTO, Car>(k.Car))))
                            .CreateMapper();
            }
        }

        public virtual IMapper ToPaymentDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Payment, PaymentDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToPayment
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PaymentDTO, Payment>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToPropertyDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Property, PropertyDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToProperty
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<PropertyDTO, Property>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToQualityDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Quality, QualityDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToQuality
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<QualityDTO, Quality>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToReturnDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Return, ReturnDTO>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrderDTO.Map<Order, OrderDTO>(k.Order)))
                            .ForMember(x => x.Crash, c => c.MapFrom(k => ToCrashDTO.Map<Crash, CrashDTO>(k.Crash.FirstOrDefault()))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToReturn
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ReturnDTO, Return>()
                            .ForMember(x => x.Order, c => c.MapFrom(k => ToOrder.Map<OrderDTO, Order>(k.Order)))
                            .ForMember(x => x.Crash, c => c.MapFrom(k => ToCrash.Map<IEnumerable<CrashDTO>, ICollection<Crash>>(new[]{k.Crash}))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToTransmissionDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Transmission, TransmissionDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToTransmission
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<TransmissionDTO, Transmission>())
                    .CreateMapper();
            }
        }
    }
}
