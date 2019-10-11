using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IRentService:IDisposable
    {
        CarDTO GetCar(int? id);

        IEnumerable<CarDTO> GetCars();
    }
}
