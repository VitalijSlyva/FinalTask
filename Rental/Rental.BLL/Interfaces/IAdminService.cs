using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for admin actions.
    /// </summary>
    public interface IAdminService:IDisposable
    {
        /// <summary>
        /// Create car.
        /// </summary>
        /// <param name="carDTO">New car</param>
        void CreateCar(CarDTO carDTO);

        /// <summary>
        /// Block car.
        /// </summary>
        /// <param name="id">Car id</param>
        void DeleteCar(int id);

        /// <summary>
        /// Update information about car.
        /// </summary>
        /// <param name="carDTO">Updated car</param>
        void UpdateCar(CarDTO carDTO);

        /// <summary>
        /// Ban user.
        /// </summary>
        /// <param name="userId">User id</param>
        void BanUser(string userId);

        /// <summary>
        /// Unban user.
        /// </summary>
        /// <param name="userId">User id</param>
        void UnbanUser(string userId);
        
        /// <summary>
        /// Create manager.
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Status</returns>
        string CreateManager(User user);

        /// <summary>
        /// Get users.
        /// </summary>
        /// <returns>Users</returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// Get roles for user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Roles</returns>
        Task<IEnumerable<string>> GetRolesAsync(string id);
    }
}
