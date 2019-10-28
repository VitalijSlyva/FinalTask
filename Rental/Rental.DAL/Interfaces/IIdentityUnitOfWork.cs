using Rental.DAL.Identity;
using System;

namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Interface for working with identity.
    /// </summary>
    public interface IIdentityUnitOfWork
    {
        ApplicationUserManager UserManager { get; }

        IClientManager ClientManager { get; }

        ApplicationRoleManager RoleManager { get; }

        /// <summary>
        /// Save changes.
        /// </summary>
        void Save();
    }
}
