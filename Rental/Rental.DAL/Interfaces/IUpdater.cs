using Rental.DAL.Entities.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IUpdateer<T> where T : class
    {
        void Update(T item);
    }
}
