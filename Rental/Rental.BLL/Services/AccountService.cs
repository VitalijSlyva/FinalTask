using Microsoft.AspNet.Identity;
using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Identity;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    /// <summary>
    /// Service for account actions.
    /// </summary>
    public class AccountService : Service, IAccountService
    {
        /// <summary>
        /// Create units and mappers for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper for converting database entities to DTO entities</param>
        /// <param name="rentUnit">Rent unit of work</param>
        /// <param name="identityUnit">Udentity unit of work</param>
        /// <param name="identityMapper">Mapper for converting identity entities to BLL classes</param>
        /// <param name="log">Service for logging</param>
        public AccountService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                 IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper, ILogService log)
                     :base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="client">User object</param>
        /// <returns>Claims information</returns>
        public async Task<ClaimsIdentity> AuthenticateAsync(User client)
        {
            try
            {
                ClaimsIdentity claims = null;
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindAsync(client.Email, client.Password);
                if (user != null)
                    claims = await IdentityUnitOfWork.UserManager
                        .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                return claims;
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "AuthenticateAsync");
                return null;
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="client">Use object</param>
        /// <returns>Status</returns>
        public async Task<string> CreateAsync(User client)
        {
            try
            {
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindByEmailAsync(client.Email);
                if (user == null)
                {
                    user = new ApplicationUser() {UserName=client.Email, Email = client.Email,Name=client.Name };
                    var result = await IdentityUnitOfWork.UserManager.CreateAsync(user, client.Password);
                    if (result.Errors.Count() > 0)
                        return result.Errors.FirstOrDefault();
                    var role = await IdentityUnitOfWork.RoleManager.FindByNameAsync("client");
                    if (role == null)
                    {
                        role = new ApplicationRole { Name = "client" };
                        await IdentityUnitOfWork.RoleManager.CreateAsync(role);
                    }
                    await IdentityUnitOfWork.UserManager.AddToRoleAsync(user.Id, "client");

                    return "";
                }
                else
                {
                    return "Пользователь уже существует;";
                }
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "CreateAsync");
            }
            return "Произошла ошибка";
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                ApplicationUser user = (await IdentityUnitOfWork.UserManager.FindByIdAsync(id));
                if (user != null)
                    return IdentityMapperDTO.ToUserDTO.Map<ApplicationUser, User>(user);

                return null;
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "GetUserAsync");

                return null;
            }
        }

        /// <summary>
        /// Test on ban for user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ban</returns>
        public bool IsBanned(string id)
        {
            return IdentityUnitOfWork.UserManager.IsInRole(id, "banned");
        }
    }
}
