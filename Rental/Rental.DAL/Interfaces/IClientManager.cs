using Rental.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IClientManager : IDisposable,ICreator<Profile>,IUpdateer<Profile> 
    {
    }
}
