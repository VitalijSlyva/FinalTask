using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface ICreator<T> where T : class
    {
        void Create(T item);
    }
}
