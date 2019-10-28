using Rental.BLL.DTO.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for standard user actons.
    /// </summary>
    public interface IAccountService
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

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        User GetUser(string id);

        /// <summary>
        /// Test on ban for user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ban</returns>
        bool IsBanned(string id);

        /// <summary>
        /// Get user id by email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Id</returns>
        string GetIdByEmail(string email);

        /// <summary>
        /// Confirm user email.
        /// </summary>
        /// <param name="userId">Id</param>
        void ConfirmEmail(string userId);

        /// <summary>
        /// Change email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        string ChangeEmail(string email, string userId, string password);

        /// <summary>
        /// Change name.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        string ChangeName(string name, string userId, string password);

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        string ChangePassword(string userId, string password);

        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="to">Email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="isBodyHtml">Body contains html</param>
        void SendMail(string to, string subject, string body, bool isBodyHtml=true);
    }
}
