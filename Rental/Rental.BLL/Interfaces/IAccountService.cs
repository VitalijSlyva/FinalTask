using Rental.BLL.DTO.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for standard user actons.
    /// </summary>
    public interface IAccountService :IDisposable
    {
        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="client">Use object</param>
        /// <returns>Status</returns>
        Task<string> CreateAsync(User client);

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="client">User object</param>
        /// <returns>Claims information</returns>
        Task<ClaimsIdentity> AuthenticateAsync(User client);

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(string id);

        User GetUser(string id);

        /// <summary>
        /// Test on ban for user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ban</returns>
        bool IsBanned(string id);

        string GetIdByEmail(string email);

        void ConfirmEmail(string userId);

        string ChangeEmail(string email, string userId, string password);

        string ChangeName(string name, string userId, string password);

        string ChangePassword(string userId, string password);
    }
}
