using Rental.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }

        IClientManager ClientManager { get; }

        ApplicationRoleManager RoleManager { get; }

        void Save();
    }
}
