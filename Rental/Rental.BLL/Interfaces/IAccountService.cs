using Rental.BLL.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IAccountService :IDisposable
    {
        Task<string> CreateAsync(User client);

        Task<ClaimsIdentity> AuthenticateAsync(User client);

        Task<User> GetUserAsync(string id);

        bool IsBanned(string id);
    }
}
