using Microsoft.AspNet.Identity;
using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class AccountService : Service, IAccountService
    {
        public AccountService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper, ILogService log)
                :base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        public async Task<ClaimsIdentity> AuthenticateAsync(User client)
        {
            try
            {
                ClaimsIdentity claims = null;
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindAsync(client.Email, client.Password);
                if (user != null)
                    claims = await IdentityUnitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                return claims;
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "AuthenticateAsync");
                return null;
            }
        }

        public async Task CreateAsync(User client)
        {
            try
            {
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindByEmailAsync(client.Email);
                if (user == null)
                {
                    user = new ApplicationUser() { UserName = client.Email, Email = client.Email };
                    var result = await IdentityUnitOfWork.UserManager.CreateAsync(user, client.Password);
                    if (result.Errors.Count() > 0)
                        throw new Exception();
                    var role = await IdentityUnitOfWork.RoleManager.FindByNameAsync("client");
                    if (role == null)
                    {
                        role = new ApplicationRole { Name = "client" };
                        await IdentityUnitOfWork.RoleManager.CreateAsync(role);
                    }
                    await IdentityUnitOfWork.UserManager.AddToRoleAsync(user.Id, "client");
                }
                else
                {
                }
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "CreateAsync");
            }
        }

        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                ApplicationUser user = (await IdentityUnitOfWork.UserManager.FindByIdAsync(id));
                if (user != null)
                    return IdentityMapperDTO.ToUserDTO.Map<ApplicationUser, User>(user);
                return null;
            }catch(Exception e)
            {
                CreateLog(e, "AccountService", "GetUserAsync");
                return null;
            }
        }
    }
}
