using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Inerface for standard methods.
    /// </summary>
    public interface IRentService:IDisposable
    {
        /// <summary>
        /// Get car by id.
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns>Car</returns>
        CarDTO GetCar(int? id);

        /// <summary>
        /// Get all cars.
        /// </summary>
        /// <returns>Cars</returns>
        IEnumerable<CarDTO> GetCars();
    }
}
