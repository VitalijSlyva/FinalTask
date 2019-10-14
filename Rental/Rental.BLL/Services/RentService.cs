using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Rent;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class RentService :Service, IRentService
    {
        public RentService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }


        public CarDTO GetCar(int? id)
        {
            try
            {
                if (id == null)
                    return null;
                var car = RentUnitOfWork.Cars.Get(id.Value);
                if (car == null)
                    return null;
                return RentMapperDTO.ToCarDTO.Map<Car, CarDTO>(car);
            }
            catch (Exception e)
            {
                CreateLog(e, "RentService", "GetCar");
                return null;
            }
        }

        public IEnumerable<CarDTO> GetCars()
        {
            try
            {
                var cars = RentUnitOfWork.Cars.Show().ToList();
                return RentMapperDTO.ToCarDTO.Map<IEnumerable<Car>, List<CarDTO>>(cars);
            }
            catch (Exception e)
            {
                CreateLog(e, "RentService", "GetCars");
                return null;
            }
        }
    }
}
