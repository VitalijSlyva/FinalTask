using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IAdminService:IDisposable
    {
        void CreateCar(CarDTO carDTO);

        void DeleteCar(int id);

        void UpdateCar(CarDTO carDTO);

        Task BanUserAsync(string userId);

        Task UnbanUserAsync(string userId);

        void CreateManager(User user);

        IEnumerable<User> GetUsers();
    }
}
