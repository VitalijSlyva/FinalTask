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

        void BanUser(string userId);

        void UnbanUser(string userId);

        string CreateManager(User user);

        IEnumerable<User> GetUsers();

        Task<IEnumerable<string>> GetRolesAsync(string id);
    }
}
